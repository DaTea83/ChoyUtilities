using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    public partial class TeaMotion {

        private bool _isBuild = false;
        public TeaMotion Build(Floater value, ETransformType transformType = ETransformType.Transform) {
            if (_isBuild) return this;
            if (transformType == ETransformType.None)
                transformType = ETransformType.Transform;
            _transformType = transformType;

            for (var i = 0; i < _transforms.Length; i++) {
                var start = _transforms[i].localPosition;
                var rotation = _transforms[i].localRotation;
                var scale = _transforms[i].localScale;

                _startPos[i] = start;
                _startRot[i] = rotation;
                _startScale[i] = scale;
                
                _endPos[i] = (float3)start + value.PositionFromTransform();
                _endRot[i] = math.mul(rotation , value.RotationFromTransform());
                _endScale[i] = scale * value.ScaleFromTransform();
            }
            _isBuild = true;
            return this;
        }

        private bool _isRun;
        public async Awaitable<TeaMotion> Run(EScheduleType scheduleType = EScheduleType.Schedule) {
            if (!_isBuild) {
                Build(new Floater(float3.zero, quaternion.identity));
                await Awaitable.EndOfFrameAsync(Token);
            }
            if (_isRun) return this;
            
            _isRun = true;
            
            try {
                switch (scheduleType) {
                
                }
                
                return this;
            }
            finally {
                _isRun = false;
            }
        }

    }

}