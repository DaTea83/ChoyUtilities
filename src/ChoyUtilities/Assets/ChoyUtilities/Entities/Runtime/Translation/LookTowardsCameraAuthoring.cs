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
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    public sealed class LookTowardsCameraAuthoring : MonoBehaviour {
        public class Baker : Baker<LookTowardsCameraAuthoring> {
            public override void Bake(LookTowardsCameraAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<LookTowardsCameraITag>(e);
            }
        }
    }

    public struct LookTowardsCameraITag : IComponentData { }

    [UpdateInGroup(typeof(TeaPostTransformSystemGroup))]
    public partial struct LookTowardsCameraISystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            if (CameraController.Instance is null || Camera.main is null) return;
            var camTargetTransform = CameraController.Instance.transform;

            foreach (var lt
                     in SystemAPI.Query<RefRW<LocalTransform>>().WithAll<LookTowardsCameraITag>()) {
                var fwd = lt.ValueRO.Position + (float3)(camTargetTransform.rotation * math.forward());
                var up = (float3)(camTargetTransform.rotation * math.up());
                lt.ValueRW.Rotation = quaternion.LookRotationSafe(fwd, up);
            }
        }
    }
}