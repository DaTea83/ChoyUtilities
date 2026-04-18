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
    /// <summary>
    ///     Add required components to the entity
    /// </summary>
    [UpdateInGroup(typeof(TeaSpawnSystemGroup), OrderLast = true)]
    public partial struct InitializeObjectAnimatorISystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (obj, entity) in
                     SystemAPI.Query<RefRO<GameObjectIData>>()
                         .WithAll<InitializeAnimatorITag>().WithEntityAccess()) {
                var instance = Object.Instantiate(obj.ValueRO.Prefab.Value.gameObject);

                ecb.AddComponent(entity, new ObjTransformIData {
                    Transform = instance.transform,
                    SmoothFollowSpeed = 0,
                    Offset = float3.zero
                });

                ecb.AddComponent(entity, new AnimatorICleanup {
                    Animator = new UnityObjectRef<Animator> {
                        Value = instance.GetComponent<Animator>()
                    }
                });

                ecb.AddComponent(entity, new AnimatorIData {
                    Animator = instance.GetComponent<Animator>()
                });

                ecb.RemoveComponent<GameObjectIData>(entity);
                ecb.RemoveComponent<InitializeAnimatorITag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}