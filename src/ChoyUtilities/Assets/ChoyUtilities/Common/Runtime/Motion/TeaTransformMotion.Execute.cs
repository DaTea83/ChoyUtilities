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

using System;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace ChoyUtilities {
    
    public partial class TeaTransformMotion {
        private bool _isBuild;

        public TeaTransformMotion Build(Floater value, float duration, EMotion motion, ETransformType transformType = ETransformType.Transform) {
            if (_isBuild) return this;
            if (transformType == ETransformType.None)
                transformType = ETransformType.Transform;
            _transformType = transformType;
            _motion = motion;
            if(duration <= 0) duration = 0.01f;
            _duration = duration;

            for (var i = 0; i < _transforms.Length; i++) {
                var start = _transforms[i].localPosition;
                var rotation = _transforms[i].localRotation;
                var scale = _transforms[i].localScale;

                _startPos[i] = start;
                _startRot[i] = rotation;
                _startScale[i] = scale;

                _endPos[i] = (float3)start + value.PositionFromTransform();
                _endRot[i] = math.mul(rotation, value.RotationFromTransform());
                _endScale[i] = scale * value.ScaleFromTransform();
            }

            _isBuild = true;
            return this;
        }

        private bool _isRun;

        public async Awaitable<TeaTransformMotion> Run(Action onDone = null) {
            if (!_isBuild) {
                Build(new Floater(float3.zero, quaternion.identity), 1f, EMotion.Linear);
                await Awaitable.EndOfFrameAsync(Token);
            }

            if (_isRun) return this;
            _isRun = true;

            var time = 0f;
            try {
                while (time < _duration) {
                    time += TIME_CONSTANT;
                    
                    var job = new TransformMotionIJob() {
                        StartPos = _startPos,
                        EndPos = _endPos,
                        StartRot = _startRot,
                        EndRot = _endRot,
                        StartScale = _startScale,
                        EndScale = _endScale,
                        TransformType = _transformType,
                        Motion = _motion,
                        T = math.saturate(time/ _duration)
                    };
                    var handle = job.Schedule(_transformAccessArray);
                    handle.Complete();

                    await Awaitable.WaitForSecondsAsync(TIME_CONSTANT, Token);
                }

                return this;
            }
            finally {
                _isRun = false;
            }
        }
    }
}