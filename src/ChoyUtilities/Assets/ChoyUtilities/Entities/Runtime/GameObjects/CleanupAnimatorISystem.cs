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
    /// <summary>
    ///     If the entity is destroyed, destroy the animator
    /// </summary>
    [UpdateInGroup(typeof(EuCCleanupSystemGroup))]
    public partial struct CleanupAnimatorISystem : ISystem {
        public void OnUpdate(ref SystemState state) {
            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);

            foreach (var (cleanup, entity)
                     in SystemAPI.Query<RefRO<AnimatorICleanup>>()
                         .WithNone<ObjTransformIData>()
                         .WithEntityAccess()) {
                if (cleanup.ValueRO.Animator.Value is not null)
                    Object.Destroy(cleanup.ValueRO.Animator.Value.gameObject);
                ecb.RemoveComponent<AnimatorICleanup>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}