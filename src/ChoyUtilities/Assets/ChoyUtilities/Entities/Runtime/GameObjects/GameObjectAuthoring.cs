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
    [RequireComponent(typeof(DestroyAuthoring))]
    public class GameObjectAuthoring : MonoBehaviour {
        public GameObject prefab;

        private class GameObjectAuthoringBaker : Baker<GameObjectAuthoring> {
            public override void Bake(GameObjectAuthoring authoring) {
                if (authoring.prefab is null) return;
                var e = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(e, new GameObjectIData {
                    Prefab = new UnityObjectRef<GameObject> {
                        Value = authoring.prefab
                    }
                });
            }
        }
    }

    public struct GameObjectIData : IComponentData {
        public UnityObjectRef<GameObject> Prefab;
    }
}