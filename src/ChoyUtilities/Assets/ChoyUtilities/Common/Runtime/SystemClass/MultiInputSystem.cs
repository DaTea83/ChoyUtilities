#if ENABLE_INPUT_SYSTEM
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using Object = UnityEngine.Object;

namespace ChoyUtilities {
    public class MultiInputSystem {
        
        private readonly string _controlScheme;
        private InputActionMap _actionMap;
        private IControlBinder _binder;
        public readonly InputDevice Device;

        private readonly InputActionAsset _asset;
        private InputUser _user;

        public MultiInputSystem(InputDevice device, InputActionAsset asset, EControlScheme controltype) {
            _user = InputUser.PerformPairingWithDevice(device);
            Device = device;
            _asset = Object.Instantiate(asset);
            _controlScheme = controltype.GetControlType();
        }

        public event Action<InputDevice, InputDeviceChange> OnBindObject;
        public event Action<InputDevice, InputDeviceChange> OnUnbindObject;

        public void EnableInput() { _actionMap.Enable(); }

        public void DisableInput() { _actionMap.Disable(); }

        public void BindObject<T>(T bindObj)
            where T : IControlBinder {
            if (_binder != null)
                UnbindObject();

            _binder = bindObj;
            _binder.Registry = this;

            var actionMapName =
                HelperCollection.InterfaceToStringName(bindObj.InputInterface, "Actions", string.Empty);
            Debug.Log($"Finding action map name of {actionMapName}");
            _actionMap = _asset.FindActionMap(actionMapName);

            if (_actionMap == null) {
                Debug.LogError($"InputActionMap '{actionMapName}' not found in the InputActionAsset.");

                return;
            }

            _user.AssociateActionsWithUser(_actionMap);
            _user.ActivateControlScheme(_controlScheme);

            HelperCollection.BindPlayerAction(_binder, _actionMap);
            EnableInput();

            OnBindObject?.Invoke(Device, InputDeviceChange.Added);
            _binder.OnBind();
        }

        public void UnbindObject() {
            _binder.Registry = null;
            _binder = null;

            foreach (var action in _actionMap)
                action.Reset();

            OnUnbindObject?.Invoke(Device, InputDeviceChange.Removed);
            DisableInput();
        }
    }

    public interface IControlBinder {
        public Type InputInterface { get; }
        MultiInputSystem Registry { get; set; }
        void OnBind();
    }
}
#endif