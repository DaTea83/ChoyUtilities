using UnityEngine;

namespace ChoyUtilities {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : GenericSingleton<CameraController> {
        public Camera Cam => GetComponent<Camera>();
    }
}