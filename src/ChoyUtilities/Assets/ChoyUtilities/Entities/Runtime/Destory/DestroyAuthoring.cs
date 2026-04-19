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
    [DisallowMultipleComponent]
    public sealed class DestroyAuthoring : MonoBehaviour {
        private class DestroyBaker : Baker<DestroyAuthoring> {
            public override void Bake(DestroyAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<DestroyIEnableableTag>(e);
                SetComponentEnabled<DestroyIEnableableTag>(e, false);
            }
        }
    }

    public struct DestroyIEnableableTag : IComponentData, IEnableableComponent { }

    [UpdateInGroup(typeof(TeaDestroySystemGroup), OrderLast = true)]
    [BurstCompile]
    public partial struct DestroyEntityISystem : ISystem {
        public void OnCreate(ref SystemState state) {
            state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var endFrameECB = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (_, entity)
                     in SystemAPI.Query<RefRO<DestroyIEnableableTag>>()
                         .WithAll<DestroyIEnableableTag>().WithEntityAccess())
                endFrameECB.DestroyEntity(entity);
        }
    }
}