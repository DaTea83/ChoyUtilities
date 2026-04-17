using Unity.Burst;
using UnityEngine.Jobs;

namespace ChoyUtilities {

    [BurstCompile]
    public struct MotionIJob : IJobParallelForTransform {

        public void Execute(int index, TransformAccess transform) {
            
        }

    }

}