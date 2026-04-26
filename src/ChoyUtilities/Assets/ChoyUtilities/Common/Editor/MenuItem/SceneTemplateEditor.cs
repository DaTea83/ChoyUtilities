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

using Unity.Scenes;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    public static class SceneTemplateEditor {
        [MenuItem(EditorCollection.UtilityWindow + "Scene/Generate Structure")]
        internal static void GenerateSceneStructure() {
            
            var eventObj = Object.FindAnyObjectByType<EventSystem>();
            if (eventObj is null) {
                _ = new GameObject("EventSystem", 
                    typeof(EventSystem), typeof(InputSystemUIInputModule));
            }

            var volumeObj = Object.FindAnyObjectByType<Volume>();
            if (volumeObj is null) {
                _ = new GameObject("Volume", 
                    typeof(Volume));
            }

            var reflectionObj = Object.FindAnyObjectByType<ReflectionProbe>();
            if (reflectionObj is null) {
                _ = new GameObject("ReflectionProbe", 
                    typeof(ReflectionProbe));
            }
            
            var objs = new[] {
                "==========< Settings & Camera >==============",
                "==========< Lighting >=======================",
                "==========< Scripts >========================",
                "==========< Static Objects >=================",
                "==========< Dynamic Objects >================",
                "==========< UI >=============================",
                "==========< Player & NPCs >==================",
                "==========< Instances & Subscenes >==========",
            };

            foreach (var obj in objs) {
                _ = new GameObject(obj);
            }
            Debug.Log("Scene structure generated.");
        }

        [MenuItem(EditorCollection.UtilityWindow + "Scene/Sort Objects")]
        internal static void SortSceneObjects() {
            
            var allRoot = SceneManager.GetActiveScene().GetRootGameObjects();
            GameObject camParent = null;
            GameObject lightParent = null;
            GameObject scriptParent = null;
            GameObject staticParent = null;
            GameObject dynamicParent = null;
            GameObject uiParent = null;
            GameObject playerParent = null;
            GameObject instanceParent = null;
            
            foreach (var obj in allRoot) {
                switch (obj.name) {
                    case "==========< Settings & Camera >==============":
                        camParent = obj;
                        break;
                    case "==========< Lighting >=======================":
                        lightParent = obj;
                        break;
                    case "==========< Scripts >========================":
                        scriptParent = obj;
                        break;
                    case "==========< Static Objects >=================":
                        staticParent = obj;
                        break;
                    case "==========< Dynamic Objects >================":
                        dynamicParent = obj;
                        break;
                    case "==========< UI >=============================":
                        uiParent = obj;
                        break;
                    case "==========< Player & NPCs >==================":
                        playerParent = obj;
                        break;
                    case "==========< Instances & Subscenes >==========":
                        instanceParent = obj;
                        break;
                }
            }
            camParent ??= new GameObject("==========< Settings & Camera >==============");
            lightParent ??= new GameObject("==========< Lighting >=======================");
            scriptParent ??= new GameObject("==========< Scripts >========================");
            staticParent ??= new GameObject("==========< Static Objects >=================");
            dynamicParent ??= new GameObject("==========< Dynamic Objects >================");
            uiParent ??= new GameObject("==========< UI >=============================");
            playerParent ??= new GameObject("==========< Player & NPCs >==================");
            instanceParent ??= new GameObject("==========< Instances & Subscenes >==========");

            foreach (var obj in allRoot) {
                switch (obj.name) {
                    case "==========< Settings & Camera >==============":
                    case "==========< Lighting >=======================":
                    case "==========< Scripts >========================":
                    case "==========< Static Objects >=================":
                    case "==========< Dynamic Objects >================":
                    case "==========< UI >=============================":
                    case "==========< Player & NPCs >==================":
                    case "==========< Instances & Subscenes >==========":
                        continue;
                }
                if (obj.TryGetComponent(out SubScene _)) continue;
                
                if (obj.isStatic) {
                    obj.transform.SetParent(staticParent.transform);
                    continue;
                }

                if (obj.TryGetComponent(out Camera _) || obj.TryGetComponent(out EventSystem _)) {
                    obj.transform.SetParent(camParent.transform);
                    continue;
                }

                if (obj.TryGetComponent(out Light _) || obj.TryGetComponent(out Volume _) ||
                    obj.TryGetComponent(out ReflectionProbe _)) {
                    obj.transform.SetParent(lightParent.transform);
                    continue;
                }
                
                if (obj.TryGetComponent(out Canvas _)) {
                    obj.transform.SetParent(uiParent.transform);
                    continue;
                }

                if (obj.TryGetComponent(out NavMeshAgent _) || obj.TryGetComponent(out Animator _)) {
                    obj.transform.SetParent(playerParent.transform);
                    continue;
                }

                if (obj.TryGetComponent(out MeshFilter _)) {
                    obj.transform.SetParent(dynamicParent.transform);
                    continue;
                }

                if (obj.TryGetComponent(out MonoBehaviour _)) {
                    obj.transform.SetParent(scriptParent.transform);
                    continue;
                }
                
                obj.transform.SetParent(dynamicParent.transform);
            }

            var tempParent = new GameObject();
            camParent.transform.SetParent(tempParent.transform);
            camParent.transform.SetSiblingIndex(0);
            
            lightParent.transform.SetParent(tempParent.transform);
            lightParent.transform.SetSiblingIndex(1);
            
            scriptParent.transform.SetParent(tempParent.transform);
            scriptParent.transform.SetSiblingIndex(2);
            
            staticParent.transform.SetParent(tempParent.transform);
            staticParent.transform.SetSiblingIndex(3);
            
            dynamicParent.transform.SetParent(tempParent.transform);
            dynamicParent.transform.SetSiblingIndex(4);
            
            uiParent.transform.SetParent(tempParent.transform);
            uiParent.transform.SetSiblingIndex(5);
            
            playerParent.transform.SetParent(tempParent.transform);
            playerParent.transform.SetSiblingIndex(6);
            
            instanceParent.transform.SetParent(tempParent.transform);
            instanceParent.transform.SetSiblingIndex(7);
            
            tempParent.transform.DetachChildren();
            Object.DestroyImmediate(tempParent);
            Debug.Log("Scene objects sorted.");
        }
    }
    
}