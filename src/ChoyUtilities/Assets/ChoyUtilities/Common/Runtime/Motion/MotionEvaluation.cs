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
using Unity.Mathematics;

namespace ChoyUtilities {
    [BurstCompile]
    public static class MotionEvaluation {
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Evaluate(this EMotion motion, float t) {
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
                EMotion.BounceOut => BounceOut(t),
                EMotion.SqrEaseInOut => SqrEaseInOut(t),
                EMotion.CubeEaseInOut => CubeEaseInOut(t),
                _ => t
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Sqr(float x) { return x * x; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Cube(float x) { return x * x * x; }

        [BurstCompile]
        private static float SqrtOut(float x) { return math.sqrt(x); }

        [BurstCompile]
        private static float CubedOut(float x) { return math.pow(x, 1.0F / 3.0F); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float QuadraticEaseInOut(float t) { return 1 - (1 - t) * (1 - t); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float Parabola(float t) { return 4f * t * (1f - t); }

        [BurstCompile]
        private static float Triangle(float t) { return 1f - 2f * math.abs(t - 0.5f); }

        [BurstCompile]
        private static float ElasticOut(float t) {
            const float halfPi = 1.57079632679f;

            return math.sin(-13f * (t * 1f) * halfPi * math.pow(2f, 10f * t) + 1f);
        }

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
        private static float SqrEaseInOut(float t) {
            var t1 = Sqr(t);
            var t2 = SqrtOut(t);

            return math.lerp(t1, t2, t);
        }

        [BurstCompile]
        private static float CubeEaseInOut(float t) {
            var t1 = Cube(t);
            var t2 = CubedOut(t);

            return math.lerp(t1, t2, t);
        }
    }
}