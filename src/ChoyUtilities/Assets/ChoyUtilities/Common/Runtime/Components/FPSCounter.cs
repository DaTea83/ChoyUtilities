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

using TMPro;
using UnityEngine;

namespace ChoyUtilities {
    [AddComponentMenu("Choy Utilities/FPS Counter")]
    [RequireComponent(typeof(TMP_Text))]
    public sealed class FPSCounter : MonoBehaviour {
        [SerializeField] private TMP_Text displayText;

        private float _frames;
        private float _time;

        private float FPS => _frames / _time;

        private void Start() {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 500;

            InvokeRepeating(nameof(UpdateText), .1f, .5f);
        }

        private void Update() {
            _time += Time.unscaledDeltaTime;
            _frames++;

            if (_time < 0.5f) return;
            _frames = 0;
            _time = 0;
        }

        private void OnValidate() { displayText ??= GetComponent<TMP_Text>(); }

        private void UpdateText() {
            if (displayText is null) return;
            displayText.text = $"{FPS:F1}";
        }
    }
}