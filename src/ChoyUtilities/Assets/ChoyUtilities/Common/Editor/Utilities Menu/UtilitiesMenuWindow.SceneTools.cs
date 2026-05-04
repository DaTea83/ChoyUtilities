// Copyright 2026 DeTea83
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {

    public sealed partial class UtilitiesMenuWindow {

        private void SetupSceneTools(VisualElement root) {
            var generateButton = root.Q<Button>(GetName("GenerateSceneStructure"))
                ?? throw new InvalidOperationException("GenerateSceneStructure button not found");
            generateButton.clicked += SceneTemplateEditor.GenerateSceneStructure;
            
            var sortButton = root.Q<Button>(GetName("SortSceneObjects"))
                ?? throw new InvalidOperationException("SortSceneObjects button not found");
            sortButton.clicked += SceneTemplateEditor.SortSceneObjects;
            
            var findMissingButton = root.Q<Button>(GetName("FindMissingScripts"))
                ?? throw new InvalidOperationException("FindMissingScripts button not found");
            findMissingButton.clicked += RemoveMissingScriptsEditor.FindMissingScripts;
            
            var removeMissingButton = root.Q<Button>(GetName("RemoveMissingScripts"))
                ?? throw new InvalidOperationException("RemoveMissingScripts button not found");
            removeMissingButton.clicked += RemoveMissingScriptsEditor.RemoveMissingScripts;
            
            var removeComponentButton = root.Q<Button>(GetName("RemoveComponent"))
                ?? throw new InvalidOperationException("RemoveComponent button not found");
            removeComponentButton.clicked += RemoveSpecificComponent;
            
            _removeComponentText = root.Q<TextField>("TextField-RemoveComponent")
                ?? throw new InvalidOperationException("RemoveComponentText field not found");
            
            return;
            string GetName(string uiName) => "Button-"+ uiName;
        }

        private TextField _removeComponentText;
        private void RemoveSpecificComponent() {
            var typeName = _removeComponentText.text;
            switch (typeName) {
                case "":
                case "Transform" or "GameObject":
                    return;
            }

            var type = FindTypeByName(typeName);
            if (type is null) {
                Debug.LogWarning($"Type '{typeName}' not found. Please enter a valid component type name.");
                return;
            }

            if (!type.IsSubclassOf(typeof(Component))) {
                Debug.LogWarning($"Type '{typeName}' is not a Component.");
                return;
            }

#if UNITY_2023_1_OR_NEWER
            var objects = FindObjectsByType(type, FindObjectsInactive.Include, FindObjectsSortMode.None);
#else
            var objects = Object.FindObjectsOfType(type, true);
#endif
            foreach (var obj in objects) {
                if (obj is not Component component) continue;
                var go = component.gameObject;
                Undo.DestroyObjectImmediate(component);
                Debug.Log($"Removed component '{typeName}' from GameObject '{go.name}'", go);
            }
        }

        private static Type FindTypeByName(string typeName) {
            var type = Type.GetType(typeName);
            if (type != null) return type;

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                type = assembly.GetType(typeName);
                if (type != null) return type;

                foreach (var t in assembly.GetTypes()) {
                    if (t.Name == typeName) return t;
                }
            }

            return null;
        }
    }

}