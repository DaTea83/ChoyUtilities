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
using Unity.Transforms;

namespace ChoyUtilities.Entities {
    [BurstCompile]
    [UpdateInGroup(typeof(EuCPreTransformSystemGroup))]
    public partial struct WaveMoveISystem : ISystem {
        private const float NoiseScale = .2F;
        private const float DepthOffset = 1f;

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            new Job {
                Time = (float)SystemAPI.Time.ElapsedTime
            }.ScheduleParallel();
        }

        [BurstCompile]
        public partial struct Job : IJobEntity {
            [ReadOnly] public float Time;

            [BurstCompile]
            private void Execute(WaveMoveIData data, ref LocalTransform lt) {
                var pos = lt.Position.GetNoiseOffsetPos(data.YOffset, Time * data.Speed,
                    data.Height, NoiseScale, DepthOffset);
                lt.Position = pos;
            }
        }
    }
}