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
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    public readonly struct ToolkitData {

        private readonly string _uxmlPath;
        private readonly string _ussPath;
        
        private VisualTreeAsset Tree => AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(_uxmlPath);
        private StyleSheet Style => AssetDatabase.LoadAssetAtPath<StyleSheet>(_ussPath);

        public ToolkitData(string name) {
            var path = EditorCollection.FindPathByName(name, "VisualTreeAsset");
            this._uxmlPath = path;
            this._ussPath = path.Replace(".uxml", ".uss");
        }
        
        public VisualElement Clone(VisualElement root = null) {
            if (Tree is null) return null;
            if (Style is null) return null;

            if (root == null) {
                Tree.CloneTree();
            }
            else {
                Tree.CloneTree(root);
                root.styleSheets.Add(Style);
            }
            return root;
        }
    }
}