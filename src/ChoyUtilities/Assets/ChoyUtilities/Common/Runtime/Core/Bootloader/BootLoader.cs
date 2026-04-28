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

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChoyUtilities.Bootloader {

    [DefaultExecutionOrder(1)]
    [DisallowMultipleComponent]
    public sealed class BootLoader : GenericSingleton<BootLoader> {

        [Tooltip("The higher the importance of that system the lower the index")]
        [field: SerializeField]
        public MonoBehaviour[] Loaders { get; set; }

        public string SceneName { get; set; } = "Default";

        public bool IsInitialized { get; private set; }

        private async void OnEnable() {
            try {
                if (SceneName == "Default" || SceneName == SceneManager.GetActiveScene().name) {
                    await BootLoaderSystem.InitSystems(Loaders);
                    IsInitialized = true;
                    Debug.Log("All loaders initialized");
                }
                else {
                    Debug.LogError("Bootloader doesn't belong to this scene");
                }
            }
            catch {
                Destroy(gameObject);
            }
        }

        protected override void OnDisable() {
            _ = BootLoaderSystem.ShutdownSystems();
            base.OnDisable();
        }

        private static class BootLoaderSystem {

            private static Stack<MonoBehaviour> _systems;

            public static async Task InitSystems(MonoBehaviour[] loaders) {
                _systems = new Stack<MonoBehaviour>();
                _systems.Clear();

                foreach (var loader in loaders) {
                    var newLoad = Instantiate(loader);

                    if (newLoad.TryGetComponent(out IBootloader bootloader)) await bootloader.Init();
                    _systems.Push(newLoad);
                }
            }

            // Last in first out
            public static async Task ShutdownSystems() {
                for (var i = _systems.Count - 1; i >= 0; i--) {
                    var system = _systems.Peek();
                    if (system.TryGetComponent(out IBootloader bootloader)) await bootloader.Shutdown();
                    Destroy(system);
                    _systems.Pop();
                }

                _systems.Clear();
                _systems = null;
            }

        }

    }

}