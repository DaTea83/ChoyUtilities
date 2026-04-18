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

using Unity.Entities;
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    public sealed class VoxelizerSingletonAuthoring : MonoBehaviour {
        [SerializeField] [Min(0.01f)] private float voxelSize = 0.05f;
        [SerializeField] [Min(0.1f)] private float voxelLife = 0.3f;
        [SerializeField] private float colorFrequency = 0.5f;
        [SerializeField] private float colorSpeed = 0.5f;
        [SerializeField] private float groundLevel;
        [SerializeField] private float gravity = 0.2f;

        public class Baker : Baker<VoxelizerSingletonAuthoring> {
            public override void Bake(VoxelizerSingletonAuthoring singletonAuthoring) {
                var e = GetEntity(TransformUsageFlags.None);

                AddComponent(e, new VoxelizerISingleton {
                    VoxelSize = singletonAuthoring.voxelSize,
                    VoxelLife = singletonAuthoring.voxelLife,
                    ColorFrequency = singletonAuthoring.colorFrequency,
                    ColorSpeed = singletonAuthoring.colorSpeed,
                    GroundLevel = singletonAuthoring.groundLevel,
                    Gravity = singletonAuthoring.gravity
                });
            }
        }
    }

    public struct VoxelizerISingleton : IComponentData {
        public float VoxelSize;
        public float VoxelLife;
        public float ColorFrequency;
        public float ColorSpeed;
        public float GroundLevel;
        public float Gravity;
    }
}