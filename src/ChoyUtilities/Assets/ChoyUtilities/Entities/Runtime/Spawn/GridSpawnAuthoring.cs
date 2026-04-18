// Copyright 2026 DaTea83
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    public sealed class GridSpawnAuthoring : MonoBehaviour {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int3 size = new(100, 1, 100);
        [SerializeField] private float3 spacing = 10;
        [SerializeField] [Min(0.001f)] private float scale = 1;

        public class Baker : Baker<GridSpawnAuthoring> {
            public override void Bake(GridSpawnAuthoring authoring) {
                DependsOn(authoring.prefab);

                var e = GetEntity(TransformUsageFlags.Renderable);
                var p = GetEntity(authoring.prefab, TransformUsageFlags.Renderable);

                AddComponent(e, new GridSpawnIData {
                    Prefab = p,
                    Size = authoring.size,
                    Spacing = authoring.spacing,
                    Scale = authoring.scale
                });
            }
        }
    }

    public struct GridSpawnIData : IComponentData {
        public Entity Prefab;
        public int3 Size;
        public float3 Spacing;
        public float Scale;
    }

    [BurstCompile]
    [UpdateInGroup(typeof(TeaSpawnSystemGroup), OrderFirst = true)]
    public partial struct SpawnGridISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            var em = state.EntityManager;

            foreach (var (grid, ltw, entity)
                     in SystemAPI.Query<RefRO<GridSpawnIData>, RefRO<LocalToWorld>>().WithEntityAccess()) {
                var startRot = ltw.ValueRO.Rotation;

                var right = math.mul(startRot, new float3(1f, 0f, 0f));
                var up = math.mul(startRot, new float3(0f, 1f, 0f));
                var forward = math.mul(startRot, new float3(0f, 0f, 1f));

                var halfWidth = grid.ValueRO.Size.x * grid.ValueRO.Spacing.x * 0.5f;
                var halfHeight = grid.ValueRO.Size.y * grid.ValueRO.Spacing.y * 0.5f;
                var halfDepth = grid.ValueRO.Size.z * grid.ValueRO.Spacing.z * 0.5f;
                var startPos = ltw.ValueRO.Position - right * halfWidth - up * halfHeight - forward * halfDepth;

                var total = grid.ValueRO.Size.x * grid.ValueRO.Size.y * grid.ValueRO.Size.z;
                using var instances = em.Instantiate(grid.ValueRO.Prefab, total, state.WorldUpdateAllocator);

                var count = 0;

                for (var x = 0; x < grid.ValueRO.Size.x; x++)
                for (var y = 0; y < grid.ValueRO.Size.y; y++)
                for (var z = 0; z < grid.ValueRO.Size.z; z++, count++) {
                    var lt = em.GetComponentData<LocalTransform>(instances[count]);

                    lt.Position = startPos +
                                  right * (x * grid.ValueRO.Spacing.x) +
                                  up * (y * grid.ValueRO.Spacing.y) +
                                  forward * (z * grid.ValueRO.Spacing.z);
                    lt.Rotation = startRot;
                    lt.Scale = grid.ValueRO.Scale;
                    em.SetComponentData(instances[count], lt);
                }

                ecb.RemoveComponent<GridSpawnIData>(entity);
            }

            ecb.Playback(em);
        }
    }
}