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

using System;
using ChoyUtilities;
using UnityEngine;

namespace Sandbox.Sandbox {

    [DisallowMultipleComponent]
    public sealed class TestMotion : MonoBehaviour {

        [SerializeField] private Transform[] sources;
        [SerializeField] private Transform target;
        [SerializeField, Min(0.01f)] private float duration = 1f;
        [SerializeField] private EMotion motionType = EMotion.Parabola;
        
        private async void Start() {
            try {
                await Awaitable.WaitForSecondsAsync(.1f);
                using var motion = new TeaMotion(sources)
                    .Build(new Floater(target), duration, motionType, ETransformType.Move);
                await motion.Run();
            }
            catch {
                Destroy(this);
            }
        }

    }

}