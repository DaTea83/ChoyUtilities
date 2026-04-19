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
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    [InitializeOnLoad]
    public class SceneTemplateEditor {
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
    }
    
}