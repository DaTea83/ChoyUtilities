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
using UnityEngine;
using UnityEngine.UIElements;

namespace ChoyUtilities {
    //TODO
    [RequireComponent(typeof(UIDocument))]
    public abstract class GenericToolkitManager<TMono, TObj> : GenericSingleton<TMono>
        where TMono : MonoBehaviour
        where TObj : ScriptableObject {
        [SerializeField] protected TObj scriptableObject;
        [SerializeField] protected UIDocument overlayUI;
        [SerializeField] protected BindingSerializable[] bindings;

        protected VisualElement Root => overlayUI?.rootVisualElement;

        private void OnEnable() { TryBindAll(); }

        protected virtual void OnValidate() { overlayUI = GetComponent<UIDocument>(); }

        protected virtual void TryBindAll() {
            if (scriptableObject is null) {
                Debug.LogWarning($"{GetType().Name}: scriptableObject is not assigned.", this);
                return;
            }

            if (overlayUI is null) {
                Debug.LogWarning($"{GetType().Name}: overlayUI is not assigned.", this);
                return;
            }

            if (Root is null) {
                Debug.LogWarning($"{GetType().Name}: rootVisualElement is null.", this);
                return;
            }

            if (bindings is null || bindings.Length == 0) {
                Debug.LogWarning($"{GetType().Name}: No bindings assigned.", this);
                return;
            }

            for (var i = 0; i < bindings.Length; i++) {
                var binding = bindings[i];

                if (string.IsNullOrWhiteSpace(binding.bindName)) {
                    Debug.LogWarning($"{GetType().Name}: Binding at index {i} has an empty element name.", this);

                    continue;
                }

                var element = binding.bindType.GetVisualElement(Root, binding.bindName);

                if (element is null) {
                    Debug.LogWarning(
                        $"{GetType().Name}: Could not find VisualElement named '{binding.bindName}' for data '{scriptableObject.name}'.",
                        this);
                    OnBindFailed(binding);

                    continue;
                }

                OnBindSuccess(binding, element);
            }
        }

        protected virtual void OnBindFailed(BindingSerializable binding) { }

        protected abstract void OnBindSuccess(BindingSerializable binding, VisualElement element);

        [Serializable]
        public struct BindingSerializable {
            public EVisualElements bindType;
            public string bindName;
        }
    }
}