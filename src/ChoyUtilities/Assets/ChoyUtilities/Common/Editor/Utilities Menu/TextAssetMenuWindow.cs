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

using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using System.IO;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    
    public sealed class TextAssetMenuWindow : EditorWindow {

        private TextAsset _content;
        private VisualElement _root;
        private static ToolkitData? _toolkitData;
        private static ToolkitData ToolkitData => _toolkitData ??= new ToolkitData("TextAssetMenu");

        public static void Show(string path, string fileName) {
            var window = GetWindow<TextAssetMenuWindow>();
            window._content = AssetDatabase.LoadAssetAtPath<TextAsset>(path) 
                             ?? throw new FileNotFoundException();
            window.titleContent = new GUIContent(fileName);
            window.minSize = new Vector2(300, 600);
            window.UpdateContentText();
            window.Show();
        }

        private void OnEnable() {
            _root = rootVisualElement;
            _root.Clear();
            ToolkitData.Clone(_root);
            _root.dataSource = this;
        }

        private void UpdateContentText() {
            var contentText = _root.Q<TextElement>("Content")
                              ?? throw new InvalidOperationException("Unable to find TextElement with name 'Content'.");
            contentText.text = _content.text;
        }

    }
}