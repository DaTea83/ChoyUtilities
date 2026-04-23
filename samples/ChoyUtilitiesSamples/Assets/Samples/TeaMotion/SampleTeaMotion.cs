// Copyright 2026 DeTea83
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

using ChoyUtilities;
using UnityEngine;

namespace Samples.TeaMotion {

    public sealed class SampleTeaMotion : MonoBehaviour {

        [SerializeField] private Transform[] targetTransforms;
        [SerializeField] private Transform endTransform;
        [SerializeField] private float duration = 1.0f;

        private async void Start() {
            try {
                using var motion = new ChoyUtilities.TeaMotion(targetTransforms).Build(new Floater(endTransform), duration, EMotion.SqrEaseIn, ETransformType.Move);
                await motion.Run();
            }
            catch {
                destroyCancellationToken.ThrowIfCancellationRequested();
            }
        }
    }

}