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
using UnityEngine;

namespace ChoyUtilities.Entities {
    [RequireComponent(typeof(DestroyAuthoring))]
    [DisallowMultipleComponent]
    public sealed class DestroyAfterTimeAuthoring : MonoBehaviour {
        [SerializeField] [Min(0.1f)] private float time;

        private class DestroyAfterTimeBaker : Baker<DestroyAfterTimeAuthoring> {
            public override void Bake(DestroyAfterTimeAuthoring authoring) {
                AddComponent(GetEntity(TransformUsageFlags.Dynamic), new DestroyTimeIData { Value = authoring.time });
            }
        }
    }

    public struct DestroyTimeIData : IComponentData {
        public float Value;
    }

    [UpdateInGroup(typeof(EuCDestroySystemGroup))]
    [UpdateBefore(typeof(DestroyEntityISystem))]
    [BurstCompile]
    public partial struct DestroyAfterTimeISystem : ISystem {
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var dt = SystemAPI.Time.DeltaTime;

            foreach (var (destroyTime, entity)
                     in SystemAPI.Query<RefRW<DestroyTimeIData>>()
                         .WithPresent<DestroyIEnableableTag>().WithEntityAccess()) {
                destroyTime.ValueRW.Value -= dt;

                if (destroyTime.ValueRW.Value > 0) continue;
                SystemAPI.SetComponentEnabled<DestroyIEnableableTag>(entity, true);
            }
        }
    }
}