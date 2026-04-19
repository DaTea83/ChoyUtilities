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
    [DisallowMultipleComponent]
    [AddComponentMenu("Choy Utilities/Follower")]
    public class SimpleFollower : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] private float3 offset;
        [SerializeField] [Range(0f, 30f)] private float smoothFollowSpeed;

        private float _factor;

        private void Update() {
            if (target is null) return;

            transform.position =
                math.lerp(transform.position, (float3)target.position + offset, _factor * Time.deltaTime);

            transform.rotation =
                math.slerp(transform.rotation, target.rotation, _factor * Time.deltaTime);
        }

        private void OnEnable() {
            if (target is null) return;
            offset = transform.position - target.position;
            _factor = smoothFollowSpeed > 0 ? smoothFollowSpeed : 1f;
        }
    }
}