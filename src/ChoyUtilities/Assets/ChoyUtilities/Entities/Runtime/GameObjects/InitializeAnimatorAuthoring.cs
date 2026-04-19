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
    [RequireComponent(typeof(GameObjectAuthoring))]
    public class InitializeAnimatorAuthoring : MonoBehaviour {
        private void OnValidate() {
            var author = GetComponent<GameObjectAuthoring>();

            if (author.prefab is null) {
                Debug.LogError($"{nameof(author.prefab)} is null");

                return;
            }

            if (!author.prefab.TryGetComponent(out Animator animator))
                Debug.LogError($"{nameof(author.prefab)} doesn't have an Animator");
        }

        private class InitializeAnimatorAuthoringBaker : Baker<InitializeAnimatorAuthoring> {
            public override void Bake(InitializeAnimatorAuthoring authoring) {
                var e = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<InitializeAnimatorITag>(e);
            }
        }
    }

    public struct AnimatorIData : IComponentData {
        public UnityObjectRef<Animator> Animator;
    }

    public struct AnimatorICleanup : ICleanupComponentData {
        public UnityObjectRef<Animator> Animator;
    }

    public struct InitializeAnimatorITag : IComponentData { }
}