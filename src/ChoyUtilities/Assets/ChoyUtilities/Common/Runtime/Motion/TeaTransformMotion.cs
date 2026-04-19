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
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace ChoyUtilities {
    
    public partial class TeaTransformMotion : IDisposable {
        
        private const float TIME_CONSTANT = 0.02f;
        
        private readonly Transform[] _transforms;

        private readonly CancellationTokenSource _tokenSource = new();
        public CancellationToken Token => _tokenSource.Token;

        private TransformAccessArray _transformAccessArray;
        private ETransformType _transformType = ETransformType.None;
        private EMotion _motion = EMotion.Linear;
        private float _duration;

        private RawSet<float3> _startPos;
        private RawSet<float3> _endPos;
        private RawSet<quaternion> _startRot;
        private RawSet<quaternion> _endRot;
        private RawSet<float3> _startScale;
        private RawSet<float3> _endScale;

        public TeaTransformMotion(Transform[] transforms) {
            _transforms = transforms;
            Init();
        }

        public TeaTransformMotion(Transform transform) : this(new[] { transform }) { }

        public TeaTransformMotion(RectTransform[] transforms) {
            _transforms = new Transform[transforms.Length];
            var i = 0;
            foreach (var r in transforms) {
                _transforms[i] = r;
                i++;
            }

            Init();
        }

        public TeaTransformMotion(RectTransform transform) : this(new[] { transform }) { }

        private void Init() {
            _transformAccessArray = new TransformAccessArray(_transforms);
            _startPos = new RawSet<float3>(_transforms.Length);
            _endPos = new RawSet<float3>(_transforms.Length);
            _startRot = new RawSet<quaternion>(_transforms.Length);
            _endRot = new RawSet<quaternion>(_transforms.Length);
            _startScale = new RawSet<float3>(_transforms.Length);
            _endScale = new RawSet<float3>(_transforms.Length);
        }

        ~TeaTransformMotion() { Dispose(false); }

        private void ReleaseUnmanagedResources() {
            if (_transformAccessArray.isCreated) _transformAccessArray.Dispose();
            if (_startPos.IsCreated) _startPos.Dispose();
            if (_endPos.IsCreated) _endPos.Dispose();
            if (_startRot.IsCreated) _startRot.Dispose();
            if (_endRot.IsCreated) _endRot.Dispose();
            if (_startScale.IsCreated) _startScale.Dispose();
            if (_endScale.IsCreated) _endScale.Dispose();
        }

        private bool _isDisposed;

        private void Dispose(bool disposing) {
            if (_isDisposed) return;
            ReleaseUnmanagedResources();
            if (disposing) {
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }

            _isDisposed = true;
        }

        public void Dispose() {
            Dispose(true);
            // Tell GC don't call deconstructor
            GC.SuppressFinalize(this);
        }
    }
}