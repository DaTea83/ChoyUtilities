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

#if UNITY_2023_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ChoyUtilities {
    public class ActivateWebcam : MonoBehaviour {
        [SerializeField] private RawImage screen;
        [SerializeField] private int width = 1280;
        [SerializeField] private int height = 720;
        [SerializeField] private int fps = 30;
        [Space] [CanBeNull] [SerializeField] private TMP_Dropdown dropdown;
        private readonly CancellationTokenSource _tokenSource = new();

        private List<WebCamDevice> _devices = new();
        private int _switchVersion;

        private WebCamTexture _webCamTexture;

        private async void Start() {
            try {
                _devices = new List<WebCamDevice>(WebCamTexture.devices);

                if (_devices.Count == 0) throw new Exception("Web Camera devices are not found");

                InitializeDropdown(_devices);

                var initialIndex = math.clamp(dropdown?.value ?? 0, 0, _devices.Count - 1);
                await SwitchToDevice(initialIndex);
            }
            catch (Exception e) {
                print(e);
            }
        }

        private void OnDestroy() {
            dropdown?.onValueChanged.RemoveListener(OnDropdownValueChanged);

            _webCamTexture?.Stop();
            _tokenSource.Cancel();
        }

        private void InitializeDropdown(IReadOnlyList<WebCamDevice> devices) {
            if (dropdown is null) return;

            dropdown.onValueChanged.RemoveListener(OnDropdownValueChanged);
            dropdown.ClearOptions();

            var options = new List<string>(devices.Count);
            options.AddRange(devices.Select(t => t.name));

            dropdown.AddOptions(options);
            dropdown.value = math.clamp(dropdown.value, 0, devices.Count - 1);
            dropdown.RefreshShownValue();
            dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int index) {
            if (index < 0 || index >= _devices.Count) return;

            _ = SwitchToDevice(index);
        }

        private async Awaitable SwitchToDevice(int index) {
            var myVersion = ++_switchVersion;

            _webCamTexture?.Stop();

            var webCamDevice = _devices[index];
            _webCamTexture = new WebCamTexture(webCamDevice.name, width, height, fps);
            _webCamTexture.Play();

            await _tokenSource.Token.AwaitableUntil(() => _webCamTexture is not null && _webCamTexture.width > 16);

            if (_tokenSource.IsCancellationRequested) return;
            if (myVersion != _switchVersion) return;

            screen.rectTransform.sizeDelta = new float2(width, height);
            screen.texture = _webCamTexture;
        }
    }
}
#endif