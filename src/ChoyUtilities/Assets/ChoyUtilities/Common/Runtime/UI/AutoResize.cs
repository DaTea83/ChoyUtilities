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

using UnityEngine;

namespace ChoyUtilities {
    [RequireComponent(typeof(RectTransform))]
    public sealed class AutoResize : MonoBehaviour {
        [SerializeField] private EFitMode eFitMode;
        private RectTransform _parentRectTransform;
        private RectTransform _rectTransform;

        private void LateUpdate() {
            if (_parentRectTransform is null) return;
            if (_rectTransform.rect.width == 0 || _rectTransform.rect.height == 0) return;
            var (width, height) = _rectTransform.GetBoundingBoxSize();
            var ratio = _parentRectTransform.rect.width / width;
            var newHeight = height * ratio;

            if (eFitMode == EFitMode.FitWidth ||
                (eFitMode == EFitMode.Expand && newHeight >= _parentRectTransform.rect.height) ||
                (eFitMode == EFitMode.Shrink && newHeight <= _parentRectTransform.rect.height)) {
                _rectTransform.offsetMin *= ratio;
                _rectTransform.offsetMax *= ratio;

                return;
            }

            ratio = _rectTransform.rect.height / height;

            _rectTransform.offsetMin *= ratio;
            _rectTransform.offsetMax *= ratio;
        }

        private void OnEnable() {
            _rectTransform = GetComponent<RectTransform>();
            _parentRectTransform = transform.parent?.GetComponent<RectTransform>();
        }

        private enum EFitMode : byte {
            Expand = 0,
            Shrink = 1,
            FitWidth = 1 << 1,
            FitHeight = 1 << 2
        }
    }
}