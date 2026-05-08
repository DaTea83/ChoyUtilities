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
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {

    public sealed partial class UtilitiesMenuWindow {
        
        private void SetupSceneConfig(VisualElement root) {
            SetupConfigTabs(root);
            SetupBootloaderConfig(root);
        }
        
        private void RefreshConfig() {
            AssetDatabase.Refresh();
            UpdateSceneNameText(_root);
            
            var isLoaderInScene = IsBootloaderInScene(out var bootloader);
            _bootloaderSceneElement.SetStatus(isLoaderInScene ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderAssetElement.SetStatus(IsBootloaderInAssets() ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootLoaderContentLabel.text = isLoaderInScene ? string.Empty : "Bootloader not found in scene";

            if (!isLoaderInScene) {
                if (_bootloaderList != null) {
                    _bootloaderList.Unbind();
                    _bootloaderList.style.display = DisplayStyle.None;
                    _bootloaderList = null;
                }
                if (_bootloaderAddButton != null) _bootloaderAddButton.style.display = DisplayStyle.None;
                if (_bootloaderRemoveButton != null) _bootloaderRemoveButton.style.display = DisplayStyle.None;
            }
            else {
                InitializeBootloaderContent(_root, true, bootloader);
                _bootloaderList.style.display = DisplayStyle.Flex;
                _bootloaderAddButton.style.display = DisplayStyle.Flex;
                _bootloaderRemoveButton.style.display = DisplayStyle.Flex;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreatePathIfAbsent() {
            if (!AssetDatabase.IsValidFolder(PrefabPath.TrimEnd('/')))
                Directory.CreateDirectory(PrefabPath);
            AssetDatabase.Refresh();
        }

        private void SetupConfigTabs(VisualElement root) {
            foreach (var tab in ConfigTabs) {
                var button = root.Q<Button>("Config-" + tab)
                    ?? throw new InvalidOperationException($"Button with name '{tab}' not found in the root VisualElement.");

                button.clicked += () => {
                    SetActiveConfigTab(root, tab);
                    RefreshConfig();
                };
            }
            SetActiveConfigTab(root, ConfigTabs[0]);
        }

        private void SetActiveConfigTab(VisualElement root, string newActive) {
            foreach (var tab in ConfigTabs) {
                var button = root.Q<Button>("Config-" + tab)
                             ?? throw new InvalidOperationException($"Button with name '{tab}' not found in the root VisualElement.");
                var panel = root.Q<VisualElement>(tab)
                            ?? throw new InvalidOperationException($"Panel with name '{tab}' not found in the root VisualElement.");
                
                var isActive = tab == newActive;
                button.EnableInClassList("config-button-active", isActive);
                panel.EnableInClassList("content-foldout-active", isActive);
                
                panel.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
            }
        }

        private StatusElement _bootloaderSceneElement;
        private StatusElement _bootloaderAssetElement;
        private Label _bootLoaderContentLabel;
        private ListView _bootloaderList;
        private Button _bootloaderAddButton;
        private Button _bootloaderRemoveButton;

        private void SetupBootloaderConfig(VisualElement root) {
            var fileName = $"{_currentScene.name}_BootLoader";
            _bootloaderSceneElement = root.Q<StatusElement>("Bootloader-Scene")
                           ?? throw new InvalidOperationException($"StatusElement with name 'Bootloader-Scene' not found in the root VisualElement.");
            
            var isLoaderInScene = IsBootloaderInScene(out var bootloader);
            var isLoaderInAssets = IsBootloaderInAssets();
            
            _bootloaderSceneElement.SetStatus(isLoaderInScene ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderSceneElement.OnClicked += () => {
                if (isLoaderInScene) return;

                if (isLoaderInAssets) {
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(PrefabPath + $"{fileName}.prefab");
                    PrefabUtility.InstantiatePrefab(prefab);
                }
                else {
                    _ = new GameObject($"{fileName}", typeof(BootLoader));
                }
                RefreshConfig();
            };
            
            _bootloaderAssetElement = root.Q<StatusElement>("Bootloader-Asset")
                                      ?? throw new InvalidOperationException($"StatusElement with name 'Bootloader-Asset' not found in the root VisualElement.");

            _bootloaderAssetElement.SetStatus(isLoaderInAssets ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderAssetElement.OnClicked += () => {

                CreatePathIfAbsent();
                if (isLoaderInScene) {
                    PrefabUtility.SaveAsPrefabAssetAndConnect(bootloader.gameObject, PrefabPath + $"{fileName}.prefab",
                        InteractionMode.AutomatedAction);
                }
                else {
                    var obj = new GameObject($"{fileName}", typeof(BootLoader));
                    PrefabUtility.SaveAsPrefabAsset(obj, PrefabPath + $"{fileName}.prefab");
                    DestroyImmediate(obj);
                }
                RefreshConfig();
            };
            
            InitializeBootloaderContent(root, isLoaderInScene, bootloader);
        }

        private void InitializeBootloaderContent(VisualElement root, bool isLoaderInScene, BootLoader bootloader) {
            _bootLoaderContentLabel = root.Q<Label>("Bootloader-Content-Label");
            _bootloaderAddButton = root.Q<Button>("Bootloader-Add");
            _bootloaderRemoveButton = root.Q<Button>("Bootloader-Remove");

            if (!isLoaderInScene) {
                _bootLoaderContentLabel.text = "Bootloader not found in scene";
                return;
            }

            _bootLoaderContentLabel.text = string.Empty;
            _bootloaderList = root.Q<ListView>("Bootloader-Content-List");
            var list = _bootloaderList;
            var serialized = new SerializedObject(bootloader);
            var prop = serialized.FindProperty("<Loaders>k__BackingField");
                
            list.makeItem = () => new LoaderItem();
            list.bindItem = (element, i) => {
                if (element is not LoaderItem item) return;
                item.Order = (i + 1).ToString();
                item.Field.BindProperty(prop.GetArrayElementAtIndex(i));
            };
            list.unbindItem = (element, i) => {
                if (element is LoaderItem item) item.Field.Unbind();
            };
            list.BindProperty(prop);
            
            _bootloaderAddButton.clicked += () => {
                prop.arraySize++;
                prop.GetArrayElementAtIndex(prop.arraySize - 1).objectReferenceValue = null;
                serialized.ApplyModifiedProperties();
            };

            _bootloaderRemoveButton.clicked += () => {
                if (prop.arraySize == 0) return;
                var index = list.selectedIndex;
                if (index < 0 || index >= prop.arraySize) {
                    prop.arraySize--;
                }
                else {
                    prop.DeleteArrayElementAtIndex(index);
                }
                serialized.ApplyModifiedProperties();
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBootloaderInScene(out BootLoader obj) {
            obj = Object.FindAnyObjectByType<BootLoader>(FindObjectsInactive.Include);
            return obj is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsBootloaderInAssets() => File.Exists(PrefabPath + $"{_currentScene.name}_BootLoader.prefab");

        private StatusElement _audioManagerSceneElement;
        private StatusElement _audioManagerAssetElement;
        private Label _audioManagerContentLabel;
        
        private void SetupAudioManagerConfig(VisualElement root) {
            var fileName = $"{_currentScene.name}_AudioManager";
            _audioManagerSceneElement = root.Q<StatusElement>("AudioManager-Scene")
                ?? throw new InvalidOperationException($"StatusElement with name 'AudioManager-Scene' not found in the root VisualElement.");
            
            
        }
    }

}