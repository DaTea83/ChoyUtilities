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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    /// <summary>
    ///     16 floats array
    /// </summary>
    [Serializable]
    [BurstCompile]
    public partial struct Floater : IComparable, IComparable<Floater>, IEquatable<Floater>,
        IFormattable, IEnumerable<float> {

        public float[] values;

        public float this[int index] {
            get {
                if ((byte)index <= 15)
                    return values[index];
                
                return 0;
            }
            set {
                if ((byte)index <= 15)
                    values[index] = value;
            }
        }
        public readonly bool IsCreated => values is not null;

        #region Constructors

        public Floater(in float[] values) {
            this.values = new float[16];
            Array.Copy(values, this.values, math.min(values.Length, 16));
        }

        public Floater(in int[] values) {
            this.values = new float[16];
            Array.Copy(values, this.values, math.min(values.Length, 16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Floater values) : this(values.values) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float value) {
            values = new float[16];
            values[0] = value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8, 
            float arg9) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8, 
            float arg9, float arg10) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8, 
            float arg9, float arg10, float arg11) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8,
            float arg9, float arg10, float arg11, float arg12) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
            values[11] = arg12;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8,
            float arg9, float arg10, float arg11, float arg12, float arg13) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
            values[11] = arg12;
            values[12] = arg13;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8,
            float arg9, float arg10, float arg11, float arg12, float arg13, float arg14) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
            values[11] = arg12;
            values[12] = arg13;
            values[13] = arg14;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8,
            float arg9, float arg10, float arg11, float arg12, float arg13, float arg14, float arg15) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
            values[11] = arg12;
            values[12] = arg13;
            values[13] = arg14;
            values[14] = arg15;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float arg1, float arg2, float arg3, float arg4, float arg5, float arg6, float arg7, float arg8,
            float arg9, float arg10, float arg11, float arg12, float arg13, float arg14, float arg15, float arg16) {
            values = new float[16];
            values[0] = arg1;
            values[1] = arg2;
            values[2] = arg3;
            values[3] = arg4;
            values[4] = arg5;
            values[5] = arg6;
            values[6] = arg7;
            values[7] = arg8;
            values[8] = arg9;
            values[9] = arg10;
            values[10] = arg11;
            values[11] = arg12;
            values[12] = arg13;
            values[13] = arg14;
            values[14] = arg15;
            values[15] = arg16;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float2 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float3 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float4 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
            values[3] = value.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Color value) {
            values = new float[16];
            values[0] = value.r;
            values[1] = value.g;
            values[2] = value.b;
            values[3] = value.a;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Vector2 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Vector3 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Vector4 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
            values[3] = value.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(int value) {
            values = new float[16];
            values[0] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(int2 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(int3 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(int4 value) {
            values = new float[16];
            values[0] = value.x;
            values[1] = value.y;
            values[2] = value.z;
            values[3] = value.w;
        }
        
        public Floater(in List<float> value) {
            values = new float[16];
            for (var i = 0; i < value.Count; i++) {
                values[i] = value[i];
            }
        }

        public Floater(in IReadOnlyList<float> value) {
            values = new float[16];
            for (var i = 0; i < value.Count; i++) {
                values[i] = value[i];
            }
        }

        public Floater(in Stack<float> value) {
            values = new float[16];
            for (var i = value.Count - 1; i >= 0; i--) {
                values[i] = value.Pop();
            }
        }
        public Floater(in Queue<float> value) { 
            values = new float[16];
            var v = value.ToArray();
            for (var i = 0; i < value.Count; i++) {
                values[i] = v[i];
            }
        }
        public Floater(in Span<float> value) { 
            values = new float[16];
            for (var i = 0; i < value.Length; i++) {
                values[i] = value[i];
            }
        }
        public Floater(in ReadOnlySpan<float> value) { 
            values = new float[16];
            for (var i = 0; i < value.Length; i++) {
                values[i] = value[i];
            }
        }
        public Floater(in NativeArray<float> value) { 
            values = new float[16];
            for (var i = 0; i < value.Length; i++) {
                values[i] = value[i];
            }
        }
        public Floater(in NativeList<float> value) { 
            values = new float[16];
            for (var i = 0; i < value.Length; i++) {
                values[i] = value[i];
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(byte value) : this((float)value) { }

        public Floater(byte[] value) {
            values = new float[16];
            Array.Copy(value, values, math.min(value.Length, 16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(sbyte value) : this((float)value) { }

        public Floater(sbyte[] value) {
            values = new float[16];
            Array.Copy(value, values, math.min(value.Length, 16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(ushort value) : this((float)value) { }

        public Floater(ushort[] value) {
            values = new float[16];
            Array.Copy(value, values, math.min(value.Length, 16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(short value) : this((float)value) { }

        public Floater(short[] value) {
            values = new float[16];
            Array.Copy(value, values, math.min(value.Length, 16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(bool value) {
            values = new float[16];
            values[0] = value ? 1f : 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(bool2 value) {
            values = new float[16];
            values[0] = value.x ? 1f : 0f;
            values[1] = value.y ? 1f : 0f;
        }

        public Floater(in bool[] value) {
            values = new float[16];
            for (var i = 0; i < math.min(value.Length, 16); i++) 
                values[i] = value[i] ? 1f : 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float4x4 value) {
            values = new float[16];
            var c0 = value.c0;
            var c1 = value.c1;
            var c2 = value.c2;
            var c3 = value.c3;
            values[0] = c0.x;
            values[1] = c0.y;
            values[2] = c0.z;
            values[3] = c0.w;
            values[4] = c1.x;
            values[5] = c1.y;
            values[6] = c1.z;
            values[7] = c1.w;
            values[8] = c2.x;
            values[9] = c2.y;
            values[10] = c2.z;
            values[11] = c2.w;
            values[12] = c3.x;
            values[13] = c3.y;
            values[14] = c3.z;
            values[15] = c3.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Matrix4x4 value) {
            values = new float[16];
            var c0 = value.GetColumn(0);
            var c1 = value.GetColumn(1);
            var c2 = value.GetColumn(2);
            var c3 = value.GetColumn(3);
            values[0] = c0.x;
            values[1] = c0.y;
            values[2] = c0.z;
            values[3] = c0.w;
            values[4] = c1.x;
            values[5] = c1.y;
            values[6] = c1.z;
            values[7] = c1.w;
            values[8] = c2.x;
            values[9] = c2.y;
            values[10] = c2.z;
            values[11] = c2.w;
            values[12] = c3.x;
            values[13] = c3.y;
            values[14] = c3.z;
            values[15] = c3.w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(double value) : this((float)value) { }

        public Floater(in double[] value) {
            values = new float[16];
            for (var i = 0; i < math.min(value.Length, 16); i++)
                values[i] = (float)value[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(half value) : this((float)value) { }

        public Floater(in half[] value) {
            values = new float[16];
            for (var i = 0; i < math.min(value.Length, 16); i++)
                values[i] = (float)value[i];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(char value) : this((float)value) { }

        public Floater(in char[] value) {
            values = new float[16];
            Array.Copy(value, values, math.min(value.Length, 16));
        }

        public Floater(string value) : this(value.ToCharArray()) { }

        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     For Transform, it will convert quaternion to euler angles before storing.
        ///     To convert Floater back to Transform use <see cref="HelperCollection.Floater" />
        ///     extension
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Transform value) {
            values = new float[16];
            var euler = math.Euler(value.rotation);
            values[0] = value.position.x;
            values[1] = value.position.y;
            values[2] = value.position.z;
            values[3] = euler.x;
            values[4] = euler.y;
            values[5] = euler.z;
            values[6] = value.localScale.x;
            values[7] = value.localScale.y;
            values[8] = value.localScale.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float3 arg1, float3 arg2) {
            values = new float[16];
            values[0] = arg1.x;
            values[1] = arg1.y;
            values[2] = arg1.z;
            values[3] = arg2.x;
            values[4] = arg2.y;
            values[5] = arg2.z;
            values[6] = 1;
            values[7] = 1;
            values[8] = 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float3 arg1, float3 arg2, float3 arg3) {
            values = new float[16];
            values[0] = arg1.x;
            values[1] = arg1.y;
            values[2] = arg1.z;
            values[3] = arg2.x;
            values[4] = arg2.y;
            values[5] = arg2.z;
            values[6] = arg3.x;
            values[7] = arg3.y;
            values[8] = arg3.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float3 arg1, quaternion arg2) {
            values = new float[16];
            var euler = math.Euler(arg2);
            values[0] = arg1.x;
            values[1] = arg1.y;
            values[2] = arg1.z;
            values[3] = euler.x;
            values[4] = euler.y;
            values[5] = euler.z;
            values[6] = 1;
            values[7] = 1;
            values[8] = 1;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(quaternion arg1) : this(float3.zero, arg1) { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(Quaternion arg1) : this(float3.zero, arg1) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Floater(float3 arg1, quaternion arg2, float3 arg3) {
            values = new float[16];
            var euler = math.Euler(arg2);
            values[0] = arg1.x;
            values[1] = arg1.y;
            values[2] = arg1.z;
            values[3] = euler.x;
            values[4] = euler.y;
            values[5] = euler.z;
            values[6] = arg3.x;
            values[7] = arg3.y;
            values[8] = arg3.z;
        }

        [Obsolete("This uses double allocation, avoid using this")]
        public Floater(in float3[] values) {
            this.values = new float[16];
            var set = new float[values.Length * 3];
            var j = 0;

            for (var i = 0; i < set.Length; i += 3, j++) {
                set[i] = values[j].x;
                set[i + 1] = values[j].y;
                set[i + 2] = values[j].z;
            }

            this.values = set;
        }

        #endregion

        #region IEnumerable

        [BurstCompile]
        public IEnumerator<float> GetEnumerator() {
            foreach (var t in values)
                yield return t;
        }

        #endregion

        #region IComparable

        [BurstCompile]
        public int CompareTo(Floater other) {
            float selfV = 0;
            float otherV = 0;

            foreach (var f in values)
                selfV += f;

            foreach (var f in other.values)
                otherV += f;

            return selfV.CompareTo(otherV);
        }

        public int CompareTo(object obj) {
            if (obj is Floater other) return CompareTo(other);

            throw new FloaterException("Object is not a FloatsSerialize.");
        }

        #endregion

        #region IFormattable

        /// <summary>
        ///     This one just return the string representation of the float array
        ///     If want want true to string, use casts to string
        /// </summary>
        /// <returns></returns>
        [BurstDiscard]
        public override string ToString() => values.ToString();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     This one just return the string representation of the float array
        ///     If want want true to string, use casts to string
        /// </summary>
        /// <returns></returns>
        [BurstDiscard]
        public string ToString(string format, IFormatProvider formatProvider) => ToString();

        #endregion

        public bool Equals(Floater other) => Equals(values, other.values);

        public override bool Equals(object obj) {
            return obj is Floater other && Equals(other);
        }

        public override int GetHashCode() {
            return values != null ? values.GetHashCode() : 0;
        }

    }

}