using System;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace ChoyUtilities {

    public static partial class HelperCollection {

        // Time-constant style smoothing
        // -DeltaTime divide timeConstant, math.max just to avoid timeConstant is 0
        // More consistent interpolation with different frame rates
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SmoothFactor(this float deltaTime, float timeConstant = 0.02f) {
            return 1f - math.exp(-deltaTime / math.max(1e-4f, timeConstant));
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 GetNoiseOffsetPos(this float3 pos, float yOffset, float time, float height,
            float noiseScale, float depthOffset) {
            pos.y = height * noise.snoise(new float2(pos.x * noiseScale + time,
                pos.z * noiseScale + time)) + yOffset * depthOffset;

            return pos;
        }

        /// <summary>
        ///     Different with normal remainder (x % y), this one will always return positive value
        /// </summary>
        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Modulo(float x, float y) {
            return (x % y + y) % y;
        }

        [BurstCompile]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Modulo(int x, int y) {
            return (x % y + y) % y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomValue(this Component obj) {
            var ran = Random.CreateFromIndex(CreateSeed(obj));

            return ran.NextFloat();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float RandomValue(this Component obj, float min, float max) {
            var ran = Random.CreateFromIndex(CreateSeed(obj));

            return ran.NextFloat(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int RandomValue(this Component obj, int min, int max) {
            var ran = Random.CreateFromIndex(CreateSeed(obj));

            return ran.NextInt(min, max);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float2 RandomValue2(this Component obj) {
            var ran = Random.CreateFromIndex(CreateSeed(obj));

            return ran.NextFloat2();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 RandomValue3(this Component obj) {
            var ran = Random.CreateFromIndex(CreateSeed(obj));

            return ran.NextFloat3();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static uint CreateSeed(Component obj) {
            // unchecked: Expect the number to overflow, but it will round it back to 0
            unchecked {
                var seed = (uint)obj.GetInstanceID();
                seed ^= (uint)Environment.TickCount;
                // Both random prime numbers
                seed = seed * 2722380223u + 1137611711u;

                if (seed == 0) seed = 1;

                return seed;
            }
        }

    }

}