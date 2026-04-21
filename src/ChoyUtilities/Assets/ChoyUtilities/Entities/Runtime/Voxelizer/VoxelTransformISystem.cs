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
using UnityEngine;
using Unity.Rendering;

namespace ChoyUtilities.Entities {
    
    [BurstCompile]
    [UpdateInGroup(typeof(TeaPreTransformSystemGroup))]
    public partial struct VoxelTransformISystem : ISystem {
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<VoxelizerISingleton>(); }

        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var job = new Job {
                DestroyLookup = SystemAPI.GetBufferLookup<DestroyBufferEntryIBuffer>(),
                Voxelizer = SystemAPI.GetSingleton<VoxelizerISingleton>(),
                ElapsedTime = (float)SystemAPI.Time.ElapsedTime,
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            job.ScheduleParallel();
        }

        [BurstCompile]
        private partial struct Job : IJobEntity {
            [NativeDisableParallelForRestriction] public BufferLookup<DestroyBufferEntryIBuffer> DestroyLookup;
            public VoxelizerISingleton Voxelizer;
            public float ElapsedTime;
            public float DeltaTime;

            [BurstCompile]
            private void Execute(Entity entity,
                ref LocalTransform lt,
                ref BoxIData box,
                ref URPMaterialPropertyBaseColor urp) {
                box.ExistTime += DeltaTime;

                if (box.ExistTime >= Voxelizer.VoxelLife) {
                    var d = DestroyLookup[entity];
                    d.Add(new DestroyBufferEntryIBuffer { Value = 1 });
                }

                box.Velocity -= Voxelizer.Gravity * DeltaTime;
                lt.Position.y += box.Velocity.y * DeltaTime;

                if (lt.Position.y < Voxelizer.GroundLevel) {
                    box.Velocity *= -1;
                    lt.Position.y = -lt.Position.y;
                }

                var lifeTime = box.ExistTime / Voxelizer.VoxelLife;
                var lifeSqr = lifeTime * lifeTime;
                lt.Scale = Voxelizer.VoxelSize * (1 - lifeSqr * lifeSqr);

                var hue = lt.Position.z * Voxelizer.ColorFrequency;
                hue = math.frac(hue + ElapsedTime * Voxelizer.ColorSpeed);
                urp.Value = (Vector4)Color.HSVToRGB(hue, 1, 1);
            }
        }
    }
}