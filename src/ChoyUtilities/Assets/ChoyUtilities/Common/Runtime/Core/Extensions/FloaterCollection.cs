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
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    [BurstCompile]
    public static partial class HelperCollection {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 PositionFromTransform(this Floater data) {
            return data.Length < 9 ? float3.zero : new float3(data[0], data[1], data[2]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static quaternion RotationFromTransform(this Floater data) {
            if (data.Length < 9) return quaternion.identity;
            var euler = new float3(data[3], data[4], data[5]);

            return quaternion.Euler(euler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float3 ScaleFromTransform(this Floater data) {
            return data.Length < 9 ? new float3(1, 1, 1) : new float3(data[6], data[7], data[8]);
        }

        public static Transform Floater(this Transform obj, Floater data) {
            obj.position = data.PositionFromTransform();
            obj.rotation = data.RotationFromTransform();
            obj.localScale = data.ScaleFromTransform();

            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Floater LogLerp(this Floater a, Floater b, float t, EMotion type) {
            var e = math.lerp(math.log(a), math.log(b), type.Evaluate(t));

            return new Floater(math.exp(e));
        }

        public static Floater Floater<T>(this T value)
            where T : struct, Enum {
            var i = Convert.ToSingle(value);

            return new Floater(i);
        }

        public static Floater Floater<T>(this T[] value)
            where T : struct, Enum {
            var set = new float[value.Length];

            for (var i = 0; i < value.Length; i++)
                set[i] = Convert.ToSingle(value[i]);

            return new Floater(set);
        }

        public static T GetEnum<T>(this Floater value)
            where T : struct, Enum {
            return (T)Enum.ToObject(typeof(T), (uint)value[0]);
        }

        public static T[] GetEnums<T>(this Floater value)
            where T : struct, Enum {
            if (!value.IsCreated)
                throw new FloaterException("Floater is not created yet.");

            var results = new T[value.Length];
            for (var i = 0; i < value.Length; i++) results[i] = (T)Enum.ToObject(typeof(T), (uint)value[i]);

            return results;
        }
    }
}