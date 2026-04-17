using UnityEngine;

namespace ChoyUtilities {

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : GenericSingleton<CameraController> {

        public Camera Cam { get; private set; }

        private void OnEnable() {
            Cam = GetComponent<Camera>();
        }

    }

}