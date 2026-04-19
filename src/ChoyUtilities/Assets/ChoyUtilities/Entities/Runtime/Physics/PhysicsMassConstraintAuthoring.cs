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
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(InitializePhysicsMassAuthoring))]
    public class PhysicsMassConstraintAuthoring : MonoBehaviour {
        [SerializeField] private bool lockX;
        [SerializeField] private bool lockY;
        [SerializeField] private bool lockZ;

        private class PhysicsMassConstraintAuthoringBaker : Baker<PhysicsMassConstraintAuthoring> {
            public override void Bake(PhysicsMassConstraintAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(e, new InitializeLockRotationIData {
                    X = authoring.lockX,
                    Y = authoring.lockY,
                    Z = authoring.lockZ
                });
            }
        }
    }

    public struct InitializeLockRotationIData : IComponentData {
        public bool X;
        public bool Y;
        public bool Z;
    }

    [UpdateInGroup(typeof(TeaManagedComponentSystem))]
    [UpdateAfter(typeof(InitializePhysicsMassISystem))]
    public partial struct InitializeLockRotationISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (initialize, mass, entity)
                     in SystemAPI.Query<RefRW<InitializeLockRotationIData>, RefRW<PhysicsMass>>().WithEntityAccess()) {
                var inInertia = mass.ValueRO.InverseInertia;
                var inX = initialize.ValueRO.X ? 0.0f : inInertia.x;
                var inY = initialize.ValueRO.Y ? 0.0f : inInertia.y;
                var inZ = initialize.ValueRO.Z ? 0.0f : inInertia.z;

                inInertia = new float3(inX, inY, inZ);
                mass.ValueRW.InverseInertia = inInertia;

                ecb.RemoveComponent<InitializeLockRotationIData>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}