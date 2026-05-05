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
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace ChoyUtilities.Editor {
    internal sealed class FancyReplaceWindow : EditorWindow {
        private const byte FONT_SIZE = 24;
        private const byte BUTTON_PER_ROW = 5;
        private const float BUTTON_PADDING = 10f;
        private const float BUTTON_HEIGHT = 100f;
        private const float HEAD_BUTTON_HEIGHT = 36f;

        private static string _assetPath;
        private static EAssetType _assetType;
        
        private static ToolkitData? _toolkitData;
        private static ToolkitData ToolkitData => _toolkitData ??= new ToolkitData("FancyReplaceWindow");

        private void OnEnable() {
            var root = rootVisualElement;
            root.Clear();
            ToolkitData.Clone(root);
            root.dataSource = this;
            SetColorField(root);
            SetButtons(root);
            SetTextures(root);
        }

        private static void SetColorField(VisualElement root) {
            var field = root.Q<ColorField>("tint");
            field.RegisterValueChangedCallback(evt => {
                FancyReplaceEditor.Asset.color = new Floater(evt.newValue);
                _ = FancyReplaceEditor.SaveData();
            });
        }

        private void SetButtons(VisualElement root) {
            var btnColor = root.Q<Button>("btn-color");

            btnColor.clicked += () => {
                FancyReplaceEditor.Asset.color = new Floater(Color.white);
                _ = FancyReplaceEditor.SaveData();
                Close();
            };
            var btnSelection = root.Q<Button>("btn-selection");

            btnSelection.clicked += () => {
                if (TryRemove())
                    _ = FancyReplaceEditor.SaveData();
                Close();
            };
        }

        private void SetTextures(VisualElement root) {
            
            var content = root.Q<VisualElement>("content");
            // Display own type textures first before global
            var typePath = FancyReplaceEditor.GetTypePath(_assetType);
            var fullPath = FancyReplaceEditor.FullTexturePath;
            
            var textures = AssetDatabase.FindAssets("t:Texture2D", new[] {
                typePath,
                fullPath
            });
            
            if (textures is null || textures.Length == 0) return;

            foreach (var textureGuid in textures) {
                var texPath = AssetDatabase.GUIDToAssetPath(textureGuid);
                var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
                var button = new Button {
                    text = string.Empty,
                    iconImage = tex,
                    
                };
                button.clicked += () => {
                    TryRemove();
                    
                    var newEntry = new MenuItemSerialize {
                        idPath = _assetPath,
                        texturePath = texPath,
                        idType = _assetType.Floater()
                    };
                    FancyReplaceEditor.AddNew(newEntry);
                    _ = FancyReplaceEditor.SaveData();
                    Close();
                };
                button.AddToClassList("selection");
                content.Add(button);
            }
        }

        internal static void ShowWindow(string assetPath, EAssetType type) {
            var window = GetWindow<FancyReplaceWindow>(FancyReplaceEditor.DEFAULT_NAME);
            window.titleContent = new GUIContent("Fancy Replace");
            window.minSize = new Vector2(300, 400);
            _assetPath = assetPath;
            _assetType = type;
            window.Show();
        }

        private static int FindPathIndex() {
            if (FancyReplaceEditor.Asset.assetModified is null ||
                FancyReplaceEditor.Asset.assetModified.Length == 0) return -1;

            return Array.FindIndex(FancyReplaceEditor.Asset.assetModified,
                data => data.idPath == _assetPath);
        }

        private static bool TryRemove() {
            var index = FindPathIndex();

            if (index == -1) return false;
            FancyReplaceEditor.RemoveAt(index);

            return true;
        }
    }
}