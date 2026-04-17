using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace ChoyUtilities {

    [BurstCompile]
    public struct MotionIJob : IJobParallelForTransform {
        
        [ReadOnly] public RawSet<float3> StartPos;
        [ReadOnly] public RawSet<float3> EndPos;
        [ReadOnly] public RawSet<quaternion> StartRot;
        [ReadOnly] public RawSet<quaternion> EndRot;
        [ReadOnly] public RawSet<float3> StartScale;
        [ReadOnly] public RawSet<float3> EndScale;
        [ReadOnly] public float T;
        [ReadOnly] public ETransformType TransformType;
        [ReadOnly] public EMotion Motion;

        public void Execute(int index, TransformAccess transform) {
            
            switch (TransformType) {
                case ETransformType.Move:
                    Pos(StartPos, EndPos, Motion, T);
                    break;
                case ETransformType.Rotate:
                    Rot(StartRot, EndRot, Motion, T);
                    break;
                case ETransformType.Scale:
                    Scale(StartScale, EndScale, Motion, T);
                    break;
                case ETransformType.Transform:
                    Pos(StartPos, EndPos, Motion, T);
                    Rot(StartRot, EndRot, Motion, T);
                    Scale(StartScale, EndScale, Motion, T);
                    break;
            }
            return;

            void Pos(RawSet<float3> start, RawSet<float3> end, EMotion motion, float t) {
                transform.localPosition = math.lerp(start[index], end[index], motion.Evaluate(t));
            }
            void Rot(RawSet<quaternion> start, RawSet<quaternion> end, EMotion motion, float t) {
                transform.localRotation = math.slerp(start[index], end[index], motion.Evaluate(t));
            }
            void Scale(RawSet<float3> start, RawSet<float3> end, EMotion motion, float t) {
                transform.localScale = math.lerp(start[index], end[index], motion.Evaluate(t));
            }
        }

    }

}