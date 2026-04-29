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
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace ChoyUtilities.Editor {

    public sealed partial class UtilitiesMenuWindow {
        
        private void SetupSceneConfig(VisualElement root) {
            var refreshButton = root.Q<Button>("Config-Refresh");
            refreshButton.clicked += RefreshConfig;
            
            SetupBootloaderConfig(root);
        }
        
        private void RefreshConfig() {
            AssetDatabase.Refresh();
            UpdateSceneNameText(_root);
            _bootloaderSceneElement.SetStatus(IsBootloaderInScene(out _) ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderAssetElement.SetStatus(IsBootloaderInAssets() ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void CreatePathIfAbsent() {
            if (!AssetDatabase.IsValidFolder(PrefabPath.TrimEnd('/')))
                Directory.CreateDirectory(PrefabPath);
            AssetDatabase.Refresh();
        }

        private StatusElement _bootloaderSceneElement;
        private StatusElement _bootloaderAssetElement;
        
        private void SetupBootloaderConfig(VisualElement root) {
            var fileName = $"{_currentScene.name}_BootLoader";
            _bootloaderSceneElement = root.Q<StatusElement>("Bootloader-Scene")
                           ?? throw new InvalidOperationException($"StatusElement with name 'Bootloader-Scene' not found in the root VisualElement.");

            _bootloaderSceneElement.SetStatus(IsBootloaderInScene(out _) ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderSceneElement.OnClicked += () => {
                if (IsBootloaderInScene(out _)) return;

                if (IsBootloaderInAssets()) {
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

            _bootloaderAssetElement.SetStatus(IsBootloaderInAssets() ? StatusElement.EStatus.Present : StatusElement.EStatus.NotFound);
            _bootloaderAssetElement.OnClicked += () => {

                CreatePathIfAbsent();
                if (IsBootloaderInScene(out var bootloader)) {
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
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsBootloaderInScene(out BootLoader obj) {
            obj = Object.FindAnyObjectByType<BootLoader>(FindObjectsInactive.Include);
            return obj is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsBootloaderInAssets() {
            return File.Exists(PrefabPath + $"{_currentScene.name}_BootLoader.prefab");
        }

        private void SetupAudioManagerConfig(VisualElement root) {
            
        }
    }

}