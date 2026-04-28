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
using System.Threading;
using UnityEngine;

namespace ChoyUtilities {

    [Icon(ICON_PATH)]
    [DisallowMultipleComponent]
    public abstract class GenericSingleton<T> : MonoBehaviour
        where T : MonoBehaviour {

        private const string ICON_PATH = "Packages/com.unity.inputsystem/InputSystem/Editor/Icons/d_Gamepad@4x.png";

        public static T Instance { get; private set; }

        protected virtual void Awake() {
            InitSingleton();
        }

        protected virtual void OnDisable() {
            CancelTask();
        }

        protected virtual void OnDestroy() {
            UnInitSingleton();
        }

        protected virtual void InitSingleton() {
            if (Instance is not null && Instance != this) {
                Destroy(gameObject);

                return;
            }

            Instance = (T)(MonoBehaviour)this;
        }

        protected virtual void UnInitSingleton() {
            if (Instance == this)
                Instance = null;
        }

        #region AsyncCancellation

        private CancellationTokenSource _cts = new();
        public CancellationToken Token => _cts.Token;
        protected event Action OnCancelTask;

        protected void CancelTask() {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            OnCancelTask?.Invoke();
        }

        #endregion

    }

}