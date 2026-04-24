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
using Unity.Mathematics;
using UnityEngine;

namespace Sandbox.Sandbox {

    [DisallowMultipleComponent]
    public sealed class TestMotion : MonoBehaviour {

        [SerializeField] private Transform[] sources;
        [SerializeField] private Transform target;
        [SerializeField, Min(0.01f)] private float duration = 1f;
        [SerializeField] private EMotion motionType = EMotion.Parabola;
        
        private float3[] _initialPos;
        private TeaMotion _motion;
        
        private async void Start() {
            try {
                _initialPos = new float3[sources.Length];

                for (var i = 0; i < sources.Length; i++) {
                    _initialPos[i] = sources[i].position;
                }

                while (true) {
                    await MotionTask();
                    await Awaitable.NextFrameAsync();
                    for (var i = 0; i < sources.Length; i++) {
                        sources[i].position = _initialPos[i];
                    }
                }
            }
            catch {
                Destroy(this);
            }
        }

        private async Awaitable MotionTask() {
            await Awaitable.WaitForSecondsAsync(.1f);
            _motion = new TeaMotion(sources)
                .Build(new Floater(target), duration, motionType, ETransformType.Move);
            await _motion.Run();
            _motion.Dispose();
        }

        private void OnDestroy() {
            _motion?.Dispose();
        }

    }

}