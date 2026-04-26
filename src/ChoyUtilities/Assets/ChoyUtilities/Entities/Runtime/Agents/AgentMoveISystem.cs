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
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ChoyUtilities.Entities {
    [BurstCompile]
    [UpdateInGroup(typeof(TeaPreTransformSystemGroup))]
    public partial struct AgentMoveISystem : ISystem {
        private const float DISTANCE_THRESHOLD = 0.1f;
        private const float DOT_THRESHOLD = 0.95f;

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            new AgentMoveIJob {
                NodeLookup = SystemAPI.GetBufferLookup<ConnectedNodeIBuffer>(true),
                LtwLookup = SystemAPI.GetComponentLookup<LocalToWorld>(true),
                Time = SystemAPI.Time.DeltaTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct AgentMoveIJob : IJobEntity {
            [ReadOnly] public BufferLookup<ConnectedNodeIBuffer> NodeLookup;
            [ReadOnly] public ComponentLookup<LocalToWorld> LtwLookup;
            public float Time;

            [BurstCompile]
            private void Execute(ref AgentMoveIEnableable move, ref AgentStatsIData stats, ref RandomIData random,
                ref LocalTransform lt) {
                var target = LtwLookup[move.CurrentNode];
                lt.GetDistanceAndDot(target, out var distanceSqr, out var dot);

                if (distanceSqr > DISTANCE_THRESHOLD) {
                    var direction = math.normalize(target.Position - lt.Position);

                    if (dot < DOT_THRESHOLD)
                        lt.Rotation = math.slerp(lt.Rotation,
                            quaternion.LookRotationSafe(direction, lt.Up()), Time * stats.RotationSpeed);
                    else
                        lt.Position += direction * stats.MoveSpeed * Time;
                }
                else {
                    if (stats.HasRestTime) {
                        move.CurrentRestTime += Time;

                        if (move.CurrentRestTime < stats.RestTime) return;
                        move.CurrentRestTime = 0;
                    }

                    var node = NodeLookup[move.CurrentNode];

                    if (node.Length == 0) return;

                    //The random system initializes earlier than the spawn system, so for first frame there's no value for random component
                    if (random.Value.state == 0) return;
                    var index = random.Value.NextInt(0, node.Length);
                    var nextNode = node[index].ConnectedNode;

                    move.CurrentNode = nextNode;
                }
            }
        }
    }
}