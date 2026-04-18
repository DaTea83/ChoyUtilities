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
    public sealed class ObjFollowerAuthoring : MonoBehaviour {
        public Transform target;
        public float3 targetOffset;
        [Range(0f, 30f)] public float smoothFollowSpeed;

        internal class Baker : Baker<ObjFollowerAuthoring> {
            public override void Bake(ObjFollowerAuthoring authoring) {
                DependsOn(authoring.target);
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new ObjTransformIData {
                    Transform = new UnityObjectRef<Transform> {
                        Value = authoring.target
                    },
                    Offset = authoring.targetOffset,
                    SmoothFollowSpeed = authoring.smoothFollowSpeed
                });
            }
        }
    }

    /// <summary>
    ///     GameObject follow entity transform
    /// </summary>
    public struct ObjTransformIData : IComponentData {
        public UnityObjectRef<Transform> Transform;
        public float3 Offset;
        public float SmoothFollowSpeed;
    }

    [UpdateInGroup(typeof(TeaPostTransformSystemGroup), OrderFirst = true)]
    public partial struct ObjFollowerISystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            var dt = SystemAPI.Time.DeltaTime;

            foreach (var (ltw, objTransformRef)
                     in SystemAPI.Query<RefRO<LocalToWorld>, RefRW<ObjTransformIData>>()) {
                var targetPos = ltw.ValueRO.Position + objTransformRef.ValueRO.Offset;

                var factor = objTransformRef.ValueRO.SmoothFollowSpeed > 0
                    ? objTransformRef.ValueRO.SmoothFollowSpeed * dt
                    : 1;
                var obj = objTransformRef.ValueRW.Transform.Value;

                obj.position = math.lerp(obj.position, targetPos, factor);
                obj.rotation = math.slerp(obj.rotation, ltw.ValueRO.Rotation, factor);
            }
        }
    }
}