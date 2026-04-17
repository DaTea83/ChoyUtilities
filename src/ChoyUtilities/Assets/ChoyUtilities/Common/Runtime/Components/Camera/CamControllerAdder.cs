using UnityEngine;

namespace ChoyUtilities {

    public class CamControllerAdder : MonoBehaviour {

        private void OnEnable() {
            var cam = Camera.main;

            if (cam is null) return;

            if (!cam.TryGetComponent<CameraController>(out var camController))
                cam.gameObject.AddComponent<CameraController>();
        }

    }

}