using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace ChoyUtilities {

    [BurstCompile]
    public struct MotionIJob : IJobParallelForTransform {
        
        [ReadOnly] public RawSet<float3> Start;
        [ReadOnly] public RawSet<float3> End;
        [ReadOnly] public float T;
        [ReadOnly] public EMotion Motion;

        public void Execute(int index, TransformAccess transform) {
            transform.position = math.lerp(Start[index], End[index], Motion.Evaluate(T));
        }

    }

}