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
using UnityEngine;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    public sealed class CameraTagAuthoring : MonoBehaviour {
        private class CameraTagBaker : Baker<CameraTagAuthoring> {
            public override void Bake(CameraTagAuthoring authoring) {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<CameraISingletonTag>(entity);
                AddComponent<InitializeCameraTargetITag>(entity);
            }
        }
    }

    public struct CameraISingletonTag : IComponentData { }

    public struct InitializeCameraTargetITag : IComponentData { }

    /// <summary>
    ///     Find any entity with the InitializeTag that doesn't have CameraTargetIData
    ///     Add the IData component
    ///     Set the transform reference of main camera to IData
    ///     Remove the InitializeTag
    /// </summary>
    [UpdateInGroup(typeof(TeaManagedComponentSystem))]
    public partial struct InitializeCameraTargetISystem : ISystem {
        public void OnCreate(ref SystemState state) { state.RequireForUpdate<InitializeCameraTargetITag>(); }

        public void OnUpdate(ref SystemState state) {
            if (CameraController.Instance is null || Camera.main is null) return;
            var camTargetTransform = CameraController.Instance.transform;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (_, entity)
                     in SystemAPI.Query<RefRO<InitializeCameraTargetITag>>()
                         .WithNone<ObjTransformIData>().WithEntityAccess()) {
                ecb.AddComponent(entity, new ObjTransformIData {
                    Transform = new UnityObjectRef<Transform> {
                        Value = camTargetTransform
                    },
                    Offset = float3.zero,
                    SmoothFollowSpeed = 0
                });
                ecb.RemoveComponent<InitializeCameraTargetITag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}