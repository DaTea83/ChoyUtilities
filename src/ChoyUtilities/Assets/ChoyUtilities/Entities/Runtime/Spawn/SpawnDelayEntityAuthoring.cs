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
    public sealed class SpawnDelayEntityAuthoring : MonoBehaviour {
        [SerializeField] private float delay = 3f;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float3 offset = new(0f, 2f, 0f);

        private class SpawnPlayerTimerAuthoringBaker : Baker<SpawnDelayEntityAuthoring> {
            public override void Bake(SpawnDelayEntityAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);
                var prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

                AddComponent(e, new SpawnDelayEntityIData {
                    Time = authoring.delay,
                    Prefab = prefab,
                    Offset = authoring.offset
                });
            }
        }
    }

    public struct SpawnDelayEntityIData : IComponentData {
        public float Time;
        public Entity Prefab;
        public float3 Offset;
    }

    [UpdateInGroup(typeof(EuCSpawnSystemGroup))]
    public partial struct SpawnDelayEntityISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (spawn, ltw, entity)
                     in SystemAPI.Query<RefRW<SpawnDelayEntityIData>, RefRO<LocalToWorld>>().WithEntityAccess()) {
                spawn.ValueRW.Time -= SystemAPI.Time.DeltaTime;

                if (spawn.ValueRO.Time > 0) continue;

                var newSpawn = ecb.Instantiate(spawn.ValueRO.Prefab);

                if (newSpawn == Entity.Null) continue;

                var nLt = LocalTransform.FromPositionRotation(ltw.ValueRO.Position + spawn.ValueRO.Offset,
                    ltw.ValueRO.Rotation);

                ecb.SetComponent(newSpawn, nLt);
                ecb.RemoveComponent<SpawnDelayEntityIData>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}