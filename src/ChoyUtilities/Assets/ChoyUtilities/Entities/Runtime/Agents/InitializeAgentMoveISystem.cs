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

namespace ChoyUtilities.Entities {
    [UpdateInGroup(typeof(EuCManagedComponentSystem))]
    public partial struct InitializeAgentMoveISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (initial, stats, entity) in
                     SystemAPI.Query<RefRO<InitializeAgentIData>, RefRO<AgentStatsIData>>()
                         .WithNone<AgentMoveIEnableable>().WithEntityAccess()) {
                ecb.AddComponent(entity, new AgentMoveIEnableable {
                    CurrentNode = initial.ValueRO.Spawn
                });

                if (stats.ValueRO.ExistTime > 0) {
                    ecb.AddComponent(entity, new DestroyTimeIData { Value = stats.ValueRO.ExistTime });
                    ecb.AddComponent(entity, new AgentMoveICleanupTag());
                }

                ecb.AddComponent<RandomIData>(entity);
                ecb.AddComponent<InitializeRandomIEnableableTag>(entity);

                ecb.RemoveComponent<InitializeAgentIData>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}