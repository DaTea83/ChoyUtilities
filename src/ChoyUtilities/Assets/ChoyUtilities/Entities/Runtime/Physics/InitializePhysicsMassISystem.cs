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
using Unity.Physics;

namespace ChoyUtilities.Entities {
    [BurstCompile]
    [UpdateInGroup(typeof(TeaManagedComponentSystem), OrderFirst = true)]
    [UpdateBefore(typeof(InitializeRandomISystem))]
    public partial struct InitializePhysicsMassISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (mass, entity)
                     in SystemAPI.Query<RefRO<PhysicsMass>>().WithAll<InitializePhysicsMassDataITag>()
                         .WithNone<PhysicsMassIData>().WithEntityAccess()) {
                ecb.AddComponent(entity, new PhysicsMassIData {
                    InverseMass = mass.ValueRO.InverseMass,
                    InverseInertia = mass.ValueRO.InverseInertia
                });
                ecb.RemoveComponent<InitializePhysicsMassDataITag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }

    public struct InitializePhysicsMassDataITag : IComponentData { }

    /// <summary>
    ///     Stores the data because changing to kinematic will set both data to zero
    /// </summary>
    /// <remarks>
    ///     The relationship between mass and inverse mass is : IM = 1 / (M * 100)
    /// </remarks>
    public struct PhysicsMassIData : IComponentData {
        public float InverseMass;
        public float3 InverseInertia;
    }
}