using UnityEngine;

namespace ChoyUtilities {

    [DisallowMultipleComponent]
    public sealed class ActivateMultiDisplay : MonoBehaviour {

        private void Start() {
            ActivateDisplay();
        }

        private static void ActivateDisplay() {
            for (var i = 1; i < Display.displays.Length; i++) Display.displays[i].Activate();
        }
    }

}