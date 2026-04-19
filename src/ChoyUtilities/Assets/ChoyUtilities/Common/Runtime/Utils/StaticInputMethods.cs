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
using UnityEngine;
using UnityEngine.InputSystem;

namespace ChoyUtilities {
    public static partial class HelperCollection {
        public static string InterfaceToStringName(Type type, string replaced = null, string replaceWith = "") {
            var name = "";

            if (replaced != null)
                name = type.Name.Replace(replaced, replaceWith);

            return name.Substring(1);
        }

        public static void BindPlayerAction<T>(T playerAction, InputActionMap map)
            where T : IControlBinder {
            var type = playerAction.InputInterface;

            foreach (var method in type.GetMethods()) {
                var actionName = method.Name.Substring(2);
                var action = map.FindAction(actionName);

                if (action != null) {
                    var delegates =
                        (Action<InputAction.CallbackContext>)method.CreateDelegate(
                            typeof(Action<InputAction.CallbackContext>), playerAction);
                    action.Reset();
                    action.started += delegates;
                    action.performed += delegates;
                    action.canceled += delegates;
                    Debug.Log($"Bind {actionName} to {action}");
                }
                else {
                    Debug.Log($"Unable to find {actionName} in {map}");
                }
            }
        }

        public static EControlScheme GetDeviceType(InputDevice device) {
            var scheme = EControlScheme.Gamepad;

            if (device is Gamepad)
                scheme = EControlScheme.Gamepad;
            else if (device is Keyboard)
                scheme = EControlScheme.Keyboard;

            return scheme;
        }

        public static string GetControlType(this EControlScheme eControl) {
            string mode = null;

            switch (eControl) {
                case EControlScheme.Keyboard:

                    mode = nameof(Keyboard);

                    break;
                case EControlScheme.Gamepad:

                    mode = nameof(Gamepad);

                    break;
            }

            return mode;
        }
    }
}

#endif