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

using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace ChoyUtilities {
    
    [AddComponentMenu("Choy Utilities/Simple Rotator")]
    [DisallowMultipleComponent]
    public class SimpleRotator : MonoBehaviour {
        [SerializeField] private EAxis rotateAxis;
        [SerializeField] private float rotateSpeed;

        private float3 _rotateDirection;
        private TransformAccessArray _transforms;
        
        private RawSet<float3> _startPos;
        private RawSet<float3> _endPos;
        private RawSet<quaternion> _startRot;
        private RawSet<quaternion> _endRot;
        private RawSet<float3> _startScale;
        private RawSet<float3> _endScale;

        private void OnEnable() {
            _transforms = new TransformAccessArray(new [] { transform });
            if ((rotateAxis & EAxis.X) != 0)
                _rotateDirection.x = 1f;

            if ((rotateAxis & EAxis.Y) != 0)
                _rotateDirection.y = 1f;

            if ((rotateAxis & EAxis.Z) != 0)
                _rotateDirection.z = 1f;

            if (rotateAxis == EAxis.None)
                enabled = false;
        }

        private JobHandle _handle;
        private void Update() {
            _startPos = new RawSet<float3>(transform.position, Allocator.TempJob);
            _endPos = new RawSet<float3>(transform.position, Allocator.TempJob);
            _startRot = new RawSet<quaternion>(transform.rotation, Allocator.TempJob);
            _endRot = new RawSet<quaternion>(math.mul(transform.rotation, quaternion.Euler(_rotateDirection)), Allocator.TempJob);
            _startScale = new RawSet<float3>(transform.localScale, Allocator.TempJob);
            _endScale = new RawSet<float3>(transform.localScale, Allocator.TempJob);
            
            var job = new TransformMotionIJob() {
                TransformType = ETransformType.Rotate,
                Motion = EMotion.Linear,
                T = rotateSpeed * Time.deltaTime,
                StartPos = _startPos,
                EndPos = _endPos,
                StartRot = _startRot,
                EndRot = _endRot,
                StartScale = _startScale,
                EndScale = _endScale
            };
            
            _handle = job.Schedule(_transforms);
        }

        private void LateUpdate() {
            _handle.Complete();
            
            if (_startPos.IsCreated) _startPos.Dispose();
            if (_endPos.IsCreated) _endPos.Dispose();
            if (_startRot.IsCreated) _startRot.Dispose();
            if (_endRot.IsCreated) _endRot.Dispose();
            if (_startScale.IsCreated) _startScale.Dispose();
            if (_endScale.IsCreated) _endScale.Dispose();
        }

        private void OnDisable() {
            if (_transforms.isCreated) _transforms.Dispose();
        }

    }
}