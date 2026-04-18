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
using Unity.Transforms;
using UnityEngine;
using ERotationOrder = Unity.Mathematics.math.RotationOrder;

namespace ChoyUtilities.Entities {
    public struct RotatorIData : IComponentData {
        /// <summary>
        ///     Order the euler axes will be applied when converting to a quaternion before applying the rotation.
        /// </summary>
        public ERotationOrder RotationOrder;

        public float3 EulerRadiansPerSecond;
    }

    [DisallowMultipleComponent]
    public sealed class RotatorAuthoring : MonoBehaviour {
        public ERotationOrder rotationOrder;
        public float3 eulerRadiansPerSecond;

        private class RotatorBaker : Baker<RotatorAuthoring> {
            public override void Bake(RotatorAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new RotatorIData {
                    RotationOrder = authoring.rotationOrder,
                    EulerRadiansPerSecond = authoring.eulerRadiansPerSecond
                });
            }
        }
    }

    [UpdateInGroup(typeof(TeaPreTransformSystemGroup))]
    [BurstCompile]
    public partial struct RotatorISystem : ISystem {
        [BurstCompile]
        public void OnUpdate(ref SystemState state) {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, rotatorData) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotatorIData>>()) {
                var rotationThisFrame = quaternion.Euler(rotatorData.ValueRO.EulerRadiansPerSecond * deltaTime,
                    rotatorData.ValueRO.RotationOrder);
                transform.ValueRW = transform.ValueRW.Rotate(rotationThisFrame);
            }
        }
    }
}