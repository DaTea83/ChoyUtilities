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

#if ENABLE_INPUT_SYSTEM
using System;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChoyUtilities.Entities {
    [DisallowMultipleComponent]
    public sealed class MouseGrabBridge : MonoBehaviour {
        [SerializeField] private InputActionAsset inputAction;
        [SerializeField] private InputActionReference mouseGrabAction;
        [SerializeField] private InputActionReference mousePositionAction;

        private float _currentInput;

        private Entity _entity;
        private EntityManager _entityManager;
        private float _previousInput;

#if UNITY_2023_1_OR_NEWER
        private async void Start() {
            try {
                if (inputAction is null) return;
                await Awaitable.EndOfFrameAsync();
                _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                _entity = _entityManager
                    .CreateEntityQuery(typeof(MouseInputISingleton)).GetSingletonEntity();
            }
            catch (Exception e) {
                Debug.LogError(e);
            }
        }
#endif

        private void OnEnable() {
            if (inputAction is null) return;

            foreach (var map in inputAction.actionMaps)
            foreach (var a in map) {
                a.Enable();
#if UNITY_EDITOR
                Debug.Log("Enabled action: " + a.name);
#endif
            }

            mousePositionAction.action.performed += OnMouseMoved;
        }

        private void OnDisable() {
            if (inputAction is null) return;

            mousePositionAction.action.performed -= OnMouseMoved;

            foreach (var map in inputAction.actionMaps)
            foreach (var a in map) {
                a.Disable();
#if UNITY_EDITOR
                Debug.Log("Disabled action: " + a.name);
#endif
            }
        }

        private void OnMouseMoved(InputAction.CallbackContext obj) {
            _previousInput = _currentInput;

            if (_entity == Entity.Null) return;
            var isPressed = mouseGrabAction.action.ReadValue<float>();
            var pos = obj.ReadValue<Vector2>();

            var input = new MouseInputISingleton {
                CurrentInput = _currentInput = isPressed,
                PreviousInput = _previousInput,
                Position = pos
            };
            _entityManager.SetComponentData(_entity, input);
        }
    }
}
#endif