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
using Unity.Physics;

namespace ChoyUtilities.Entities {
    [UpdateInGroup(typeof(EuCManagedComponentSystem), OrderFirst = true)]
    [UpdateBefore(typeof(InitializeRandomISystem))]
    public partial struct InitializePhysicsGravityISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (_, entity)
                     in SystemAPI.Query<RefRO<PhysicsMass>>()
                         .WithNone<PhysicsGravityFactor>().WithEntityAccess())
                ecb.AddComponent(entity, new PhysicsGravityFactor {
                    Value = 1
                });

            ecb.Playback(state.EntityManager);
        }
    }
}