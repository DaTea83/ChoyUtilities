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
    [RequireComponent(typeof(RandomAuthoring))]
    public class SphereSpawnAuthoring : MonoBehaviour {
        public GameObject prefab;
        public float radius = 10f;
        public byte amount = 10;
        public float height = 10f;
        [Range(0, 2f)] public float speed = .2f;

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public class Baker : Baker<SphereSpawnAuthoring> {
            public override void Bake(SphereSpawnAuthoring authoring) {
                if (authoring.prefab is null || authoring.radius <= 0) return;

                var entity = GetEntity(TransformUsageFlags.Renderable);
                var prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic);

                AddComponent(entity, new SphereSpawnIData {
                    Prefab = prefab,
                    Radius = authoring.radius,
                    Amount = authoring.amount,
                    Height = authoring.height,
                    Speed = authoring.speed
                });
            }
        }
    }

    public struct SphereSpawnIData : IComponentData {
        public Entity Prefab;
        public float Radius;
        public byte Amount;
        public float Height;
        public float Speed;
    }

    [BurstCompile]
    [UpdateInGroup(typeof(TeaSpawnSystemGroup), OrderFirst = true)]
    public partial struct SpawnInSphereISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            var em = state.EntityManager;

            foreach (var (sphere, random, ltw, entity)
                     in SystemAPI.Query<RefRO<SphereSpawnIData>, RefRW<RandomIData>, RefRO<LocalToWorld>>()
                         .WithEntityAccess()) {
                using var instances = em.Instantiate(sphere.ValueRO.Prefab, sphere.ValueRO.Amount,
                    state.WorldUpdateAllocator);

                var rng = random.ValueRW.Value;

                for (var i = 0; i < sphere.ValueRO.Amount; i++) {
                    var lt = em.GetComponentData<LocalTransform>(instances[i]);
                    var dir = rng.NextFloat3Direction();
                    var dist = sphere.ValueRO.Radius * math.pow(rng.NextFloat(), 0.3333f);
                    var newPos = ltw.ValueRO.Position + dir * dist;

                    lt.Position = newPos;
                    em.SetComponentData(instances[i], lt);

                    ecb.AddComponent(instances[i], new WaveMoveIData {
                        YOffset = newPos.y,
                        Height = sphere.ValueRO.Height,
                        Speed = sphere.ValueRO.Speed
                    });
                }

                ecb.RemoveComponent<SphereSpawnIData>(entity);
            }

            ecb.Playback(em);
        }
    }
}