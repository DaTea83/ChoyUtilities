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
    public abstract class UiHelper : MonoBehaviour {
        [SerializeField] [Min(0.01f)] protected float transitionTime;

        protected RectTransform RectTransform;

        protected virtual void OnEnable() { RectTransform = GetComponent<RectTransform>(); }

        public abstract void OnSpawn();

        public abstract float OnStartOpen();

        public abstract void OnEndOpen();

        public abstract float OnStartClose();

        public abstract void OnEndClose();
    }
}