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
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {
    public sealed partial class UtilitiesMenuWindow : EditorWindow {
        
        private Scene _currentScene;
        private const string GIT_LINK = "https://github.com/DaTea83/ChoyUtilities";
        
        private static readonly string[] Tabs = { "Status", "Packages", "SceneConfig", "SceneTools" };
        private static readonly ToolkitData ToolkitData = new("UtilitiesMenu");

        private void OnEnable() {
            var root = InitializeRootVisualElement();
            SetupTabs(root);
            SetupTitle(root);
            SetupSceneTools(root);
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
            
            var logButton = root.Q<Button>(GetName("ChangeLog"));
            var logPath = PackagePath("CHANGELOG.md");
            logButton.clicked += () => TextAssetMenuWindow.Show(logPath, "ChangeLog");
            
            var docButton = root.Q<Button>(GetName("Doc"));
            var docPath = PackagePath("DOCUMENTATION.md");
            docButton.clicked += () => TextAssetMenuWindow.Show(docPath, "Documentation");
            
            var versionText = root.Q<TextElement>(GetName("Version"));
            var json = PackagePath("package.json");
            var package = ContentText(json);
            versionText.text = package;
            
            var projectText = root.Q<TextElement>(GetName("ProjectName"));
            projectText.text = "Project: \n" + EditorCollection.ProjectFolderName;
            
            var sceneText = root.Q<TextElement>(GetName("SceneName"));
            _currentScene = SceneManager.GetActiveScene();
            sceneText.text = "Current Scene: \n" + _currentScene.name;

            return;
            string GetName(string uiName) => "Top-" + uiName;

            string PackagePath(string fileName) {
                var path = EditorCollection.FindPathByName("CommonPackage", "TextAsset");
                path = path.Replace("CommonPackage.txt", string.Empty);
                return path + fileName;
            }
        }

        // I give up using the JsonUtility way, let just brute force cut the string
        private static string ContentText(string json) {
            var package = AssetDatabase.LoadAssetAtPath<TextAsset>(json).ToString();
            package = Array.Find(package.Split('\n'), l => l.TrimStart().StartsWith("\"version\""))
                ?.Trim().TrimEnd(',') ?? string.Empty;
            package = package.Replace("\"", string.Empty);
            package = package.Replace("v", "V");
            return package;
        }

    }
}