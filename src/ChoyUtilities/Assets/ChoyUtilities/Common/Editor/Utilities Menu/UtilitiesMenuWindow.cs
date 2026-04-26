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
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    public sealed class UtilitiesMenuWindow : EditorWindow {
        
        private static readonly ToolkitData ToolkitData = new("UtilitiesMenu");
        
        [MenuItem(EditorCollection.UtilityWindow + "Menu", priority = 0)]
        private static void ShowWindow() {
            var window = GetWindow<UtilitiesMenuWindow>();
            window.titleContent = new GUIContent("Choy Utilities");
            window.minSize = new Vector2(600, 600);
            window.Show();
        }
 
        private void OnEnable() {
            var root = InitializeRootVisualElement();
        }

        private VisualElement InitializeRootVisualElement() {
            var root = this.rootVisualElement;
            root.Clear();
            ToolkitData.Clone(root);
            root.dataSource = this;
            return root;
        }
    }
}