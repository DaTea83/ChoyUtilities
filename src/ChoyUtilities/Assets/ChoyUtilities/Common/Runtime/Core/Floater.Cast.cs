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
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    public partial struct Floater {
        #region Casting

        public static implicit operator float(Floater value) { return value.values[0]; }

        public static implicit operator float[](Floater value) { return value.values; }

        public static implicit operator List<float>(Floater value) { return new List<float>(value.values); }

        public static implicit operator int[](Floater value) {
            if (value.values == null || value.values.Length == 0) throw new FloaterException("Empty Floater found");
            var set = new int[value.values.Length];
            for (var i = 0; i < value.values.Length; i++)
                set[i] = (int)value.values[i];

            return set;
        }

        //public static implicit operator Array(FloatsSerialize value) => value.Values;
        public static implicit operator float2(Floater value) {
            if (value.values is null || value.values.Length < 2)
                throw new FloaterException("Value must be more than 2 floats");
            var set = new float2(value.values[0], value.values[1]);

            return set;
        }

        public static implicit operator float3(Floater value) {
            if (value.values is null || value.values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new float3(value.values[0], value.values[1], value.values[2]);

            return set;
        }

        public static implicit operator float4(Floater value) {
            if (value.values is null || value.values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new float4(value.values[0], value.values[1], value.values[2], value.values[3]);

            return set;
        }

        public static implicit operator Vector3(Floater value) {
            if (value.values is null || value.values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new Vector3(value.values[0], value.values[1], value.values[2]);

            return set;
        }

        public static implicit operator Color(Floater value) {
            if (value.values is null || value.values.Length < 4) return Color.white;
            var set = new Color(value.values[0], value.values[1], value.values[2], value.values[3]);

            return set;
        }

        public static implicit operator char[](Floater value) {
            if (value.values == null || value.values.Length == 0) return Array.Empty<char>();
            var set = new char[value.values.Length];

            for (var i = 0; i < value.values.Length; i++) {
                var j = (char)value.values[i];
                set[i] = j;
            }

            return set;
        }

        public static implicit operator string(Floater value) {
            char[] set = value;
            var s = string.Empty;

            for (var i = 0; i < set.Length && set[i] != 0; i++) s += set[i];

            return s;
        }

        public static implicit operator NativeArray<float>.ReadOnly(Floater value) {
            return new NativeArray<float>(value.values, Allocator.Temp).AsReadOnly();
        }

        public static implicit operator RawSet<float>(Floater value) {
            return new RawSet<float>(value.values, Allocator.Temp);
        }

        #endregion
    }
}