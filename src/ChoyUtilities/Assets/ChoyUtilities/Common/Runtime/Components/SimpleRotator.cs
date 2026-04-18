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

using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    [AddComponentMenu("Choy Utilities/Simple Rotator")]
    [DisallowMultipleComponent]
    public class SimpleRotator : MonoBehaviour {
        [SerializeField] private EAxis rotateAxis;
        [SerializeField] private float rotateSpeed;

        private float3 _rotateDirection;

        private void Start() {
            if ((rotateAxis & EAxis.X) != 0)
                _rotateDirection.x = 1f;

            if ((rotateAxis & EAxis.Y) != 0)
                _rotateDirection.y = 1f;

            if ((rotateAxis & EAxis.Z) != 0)
                _rotateDirection.z = 1f;

            if (rotateAxis == EAxis.None)
                enabled = false;
        }

        private void Update() { transform.Rotate(_rotateDirection * (rotateSpeed * Time.deltaTime)); }
    }
}