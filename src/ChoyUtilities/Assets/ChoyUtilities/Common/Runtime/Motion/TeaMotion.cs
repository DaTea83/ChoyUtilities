using System;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace ChoyUtilities {

    public partial class TeaMotion : IDisposable {

        private Transform[] _transforms;

        private readonly CancellationTokenSource _tokenSource = new();
        public CancellationToken Token => _tokenSource.Token;

        private TransformAccessArray _transformAccessArray;
        private RawSet<float3> _start;
        private RawSet<float3> _end;

        public TeaMotion(Transform[] transforms) {
            _transforms = transforms;
            _transformAccessArray = new TransformAccessArray(_transforms);
            _start = new RawSet<float3>(_transforms.Length);
            _end = new RawSet<float3>(_transforms.Length);
        }

        public TeaMotion(Transform transform) : this(new[] { transform }) { }

        public TeaMotion(RectTransform[] transforms) {
            _transforms = new Transform[transforms.Length];
            var i = 0;
            foreach (var r in transforms) {
                _transforms[i] = r;
                i++;
            }
        }
        
        public TeaMotion(RectTransform transform) : this(new[] { transform }){ }

        ~TeaMotion() {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources() {
            if (_transformAccessArray.isCreated) _transformAccessArray.Dispose();
            if (_start.IsCreated) _start.Dispose();
            if (_end.IsCreated) _end.Dispose();
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