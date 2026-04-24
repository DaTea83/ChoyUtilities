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

using System.Runtime.CompilerServices;
using Unity.Burst;
using static Unity.Mathematics.math;

namespace ChoyUtilities {
    
    [BurstCompile]
    public static class MotionEvaluation {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Evaluate(this EMotion motion, float t) {
            if (t <= 0f) t = 1f;
            return motion switch {
                EMotion.Linear => t,
                EMotion.SqrEaseIn => Sqr(t),
                EMotion.CubeEaseIn => Cube(t),
                EMotion.SqrtEaseOut => SqrtOut(t),
                EMotion.CubedEaseOut => CubedOut(t),
                EMotion.QuadraticEaseOut => QuadraticEaseInOut(t),
                EMotion.Parabola => Parabola(t),
                EMotion.Triangle => Triangle(t),
                EMotion.ElasticOut => ElasticOut(t),
                EMotion.ElasticIn => ElasticIn(t),
                EMotion.BounceOut => BounceOut(t),
                EMotion.SqrSnap => SqrSnap(t),
                EMotion.CubeSnap => CubeSnap(t),
                EMotion.SineWave => SineWave(t),
                EMotion.CosWave => CosWave(t),
                EMotion.BurstOut => BurstOut(t),
                EMotion.BurstIn => BurstIn(t),
                _ => t
            };
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Sqr(float x) => x * x;

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Cube(float x) => x * x * x;

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float SqrtOut(float x) => sqrt(x);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CubedOut(float x) => pow(x, 1.0F / 3.0F);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float QuadraticEaseInOut(float t) => 1 - (1 - t) * (1 - t);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Parabola(float t) => 4f * t * (1f - t);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Triangle(float t) => 1f - 2f * abs(t - 0.5f);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ElasticOut(float t) {
            const float twoThirdPI = 2 * PI / 3;
            return pow(2, -10 * t) * sin((t * 10 - 0.75f) * twoThirdPI) + 1f;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float ElasticIn(float t) {
            const float twoThirdPI = 2 * PI / 3;
            return -pow(2, 10 * t - 10) * sin((t * 10 - 10.75f) * twoThirdPI);
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float BounceOut(float t) {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;

            switch (t) {
                case < 1 / d1:

                    return n1 * t * t;

                case < 2 / d1:

                    t -= 1.5f / d1;

                    return n1 * t * t + 0.75f;

                default: {
                    if (t < 2.5 / d1) {
                        t -= 2.25f / d1;

                        return n1 * t * t + 0.9375f;
                    }

                    t -= 2.625f / d1;

                    return n1 * t * t + 0.984375f;
                }
            }
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float SqrSnap(float t) => t < 0.5f ? Sqr(t) : SqrtOut(t);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CubeSnap(float t) => t < 0.5f ? Cube(t) : CubedOut(t);
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float SineWave(float t) => sin(t * PI * 2);
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float CosWave(float t) => 1f - cos(t * PI * 2);

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float BurstOut(float t) => 1 - sqrt(1 - pow(t, 2));
        
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float BurstIn(float x) => sqrt(1 - pow(x - 1, 2));
    }
}