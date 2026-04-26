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

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    public sealed class UtilitiesMenuWindow : EditorWindow {
        
        private const string GIT_LINK = "https://github.com/DaTea83/ChoyUtilities";
        
        private static readonly string[] Tabs = { "Status", "Packages", "SceneConfig", "SceneTools" };
        private static readonly ToolkitData ToolkitData = new("UtilitiesMenu");

        private void OnEnable() {
            var root = InitializeRootVisualElement();
            SetupTabs(root);
            SetupTitle(root);
        }

        [MenuItem(EditorCollection.UtilityWindow + "Menu", priority = 0)]
        private static void ShowWindow() {
            var window = GetWindow<UtilitiesMenuWindow>();
            window.titleContent = new GUIContent("Choy Utilities");
            window.minSize = new Vector2(600, 600);
            window.Show();
        }

        private VisualElement InitializeRootVisualElement() {
            var root = rootVisualElement;
            root.Clear();
            ToolkitData.Clone(root);
            root.dataSource = this;
            return root;
        }

        private static void SetupTabs(VisualElement root) {
            foreach (var tab in Tabs) {
                var button = root.Q<Button>("Tab-" + tab)
                             ?? throw new InvalidOperationException(
                                 $"Unable to find button with name 'Tab-{tab}' in root element.");
                button.clicked += () => SetActiveTab(root, tab);
            }
            SetActiveTab(root, Tabs[0]);
        }

        private static void SetActiveTab(VisualElement root, string newActive) {
            foreach (var tab in Tabs) {
                
                var button = root.Q<Button>("Tab-" + tab)
                             ?? throw new InvalidOperationException(
                                 $"Unable to find button with name 'Tab-{tab}' in root element.");
                var panel = root.Q<VisualElement>("Content-" + tab)
                            ?? throw new InvalidOperationException(
                                $"Unable to find panel with name 'Content-{tab}' in root element.");

                var isActive = tab == newActive;
                button.EnableInClassList("tab-active", isActive);
                panel.EnableInClassList("content-active", isActive);
                
                panel.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        private void SetupTitle(VisualElement root) {
            var gitButton = root.Q<Button>(GetName("Github"));
            gitButton.clicked += () => Application.OpenURL(GIT_LINK);
            
            var projectText = root.Q<TextElement>(GetName("ProjectName"));
            projectText.text = EditorCollection.ProjectFolderName;

            return;
            string GetName(string uiName) => "Top-" + uiName;
        }
    }
}