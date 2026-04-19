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

using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    internal static class RemoveMissingScriptsEditor {
        [MenuItem(EditorCollection.UtilityWindow + "Missing Scripts/Find")]
        internal static void FindMissingScripts() {
#if UNITY_2023_1_OR_NEWER
            foreach (var gameObject in Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
#else
            foreach (var gameObject in GameObject.FindObjectsOfType<GameObject>(true))
#endif
            foreach (var component in gameObject.GetComponentsInChildren<Component>()) {
                if (component is not null) continue;
                Debug.Log($"GameObject: {gameObject.name} has missing Script", gameObject);

                break;
            }
        }

        [MenuItem(EditorCollection.UtilityWindow + "Missing Scripts/Remove")]
        internal static void RemoveMissingScripts() {
#if UNITY_2023_1_OR_NEWER
            foreach (var gameObject in Object.FindObjectsByType<GameObject>(FindObjectsInactive.Include,
                         FindObjectsSortMode.None))
#else
            foreach (var gameObject in GameObject.FindObjectsOfType<GameObject>(true))
#endif
            foreach (var component in gameObject.GetComponentsInChildren<Component>()) {
                if (component is not null) continue;
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                Debug.Log($"Removing {gameObject.name}'s missing Script", gameObject);
            }
        }
    }
}