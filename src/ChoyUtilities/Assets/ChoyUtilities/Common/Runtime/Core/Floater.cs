using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    [Serializable]
    [BurstCompile]
    public partial struct Floater : IEquatable<Floater>,
        IComparable, IComparable<Floater>, IFormattable, IEnumerable<float> {
        
        public float[] values;

        #region Constructors

        public Floater(float[] values) { this.values = values; }
        public Floater(int[] values) {
            var set = new float[values.Length];
            Array.Copy(values, set, values.Length);
            this.values = set;
        }

        public Floater(Floater values) : this(values.values) { }
        public Floater(float value) : this(new[] { value }) { }
        public Floater(float2 value) : this(new[] { value.x, value.y }) { }
        public Floater(float3 value) : this(new[] { value.x, value.y, value.z }) { }
        public Floater(float4 value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public Floater(Color value) : this(new[] { value.r, value.g, value.b, value.a }) { }
        public Floater(Quaternion value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public Floater(quaternion value) {
            var v = value.value;
            values = new[] { v.x, v.y, v.z, v.w };
        }
        public Floater(Vector2 value) : this(new[] { value.x, value.y }) { }
        public Floater(Vector3 value) : this(new[] { value.x, value.y, value.z }) { }
        public Floater(Vector4 value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public Floater(int value) : this(new[] { value }) { }
        public Floater(int2 value) : this(new[] { value.x, value.y }) { }
        public Floater(int3 value) : this(new[] { value.x, value.y, value.z }) { }
        public Floater(int4 value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public Floater(List<float> value) : this(value.ToArray()) { }
        public Floater(Stack<float> value) : this(value.ToArray()) { }
        public Floater(Queue<float> value) : this(value.ToArray()) { }
        public Floater(NativeArray<float> value) : this(value.ToArray()) { }
        public Floater(NativeList<float> value) : this(value.ToArray(Allocator.Temp)) { }
        public Floater(byte value) : this(new int[] { value }) { }
        public Floater(byte[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(sbyte value) : this(new int[] { value }) { }
        public Floater(sbyte[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(ushort value) : this(new int[] { value }) { }
        public Floater(ushort[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(short value) : this(new int[] { value }) { }
        public Floater(short[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(bool value) { values = new[] { value ? 1f : 0f }; }
        public Floater(bool2 value) { values = new[] { value.x ? 1f : 0f, value.y ? 1f : 0f }; }
        public Floater(bool[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) set[i] = value[i] ? 1f : 0f;

            values = set;
        }
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
        public Floater(double value) : this(new[] { (float)value }) { }
        public Floater(double[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(half value) : this(new[] { (float)value }) { }
        public Floater(half[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(Enum value) : this(new[] { (int)Convert.ToUInt32(value) }) { }
        public Floater(Enum[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) set[i] = (int)Convert.ToUInt32(value[i]);

            values = set;
        }
        public Floater(char value) : this(new[] { (float)value }) { }
        public Floater(char[] value) {
            var set = new float[value.Length];
            if (set == null) throw new FloaterException(nameof(set));
            for (var i = 0; i < value.Length; i++) {
                var j = (int)value[i];
                set[i] = j;
            }
            values = set;
        }
        public Floater(string value) : this(value.ToCharArray()) { }
        /// <summary>
        /// </summary>
        /// <param name="value">
        ///     For Transform, it will convert quaternion to euler angles before storing.
        ///     To convert FloatsSerialize back to Transform use <see cref="HelperCollection.Floater" />
        ///     extension
        /// </param>
        public Floater(Transform value) {
            var set = new float[9];
            var euler = math.Euler(value.rotation);
            set[0] = value.position.x;
            set[1] = value.position.y;
            set[2] = value.position.z;
            set[3] = euler.x;
            set[4] = euler.y;
            set[5] = euler.z;
            set[6] = value.localScale.x;
            set[7] = value.localScale.y;
            set[8] = value.localScale.z;
            values = set;
        }

        #endregion

        #region IEquatable, IEnumerable

        public bool Equals(Floater other) { return Equals(values, other.values); }
        public IEnumerator<float> GetEnumerator() {
            foreach (var t in values)
                yield return t;
        }
        public override bool Equals(object obj) { return obj is Floater other && Equals(other); }
        public override int GetHashCode() { return values is not null ? values.GetHashCode() : 0; }

        #endregion

        #region IComparable

        public int CompareTo(Floater other) {
            float selfV = 0;
            float otherV = 0;
            foreach (var f in values) selfV += f;

            foreach (var f in other.values) otherV += f;

            return selfV.CompareTo(otherV);
        }

        public int CompareTo(object obj) {
            if (obj is Floater other) return CompareTo(other);
            throw new FloaterException("Object is not a FloatsSerialize.");
        }

        #endregion

        #region IFormattable

        public override string ToString() {
            char[] set = new Floater(values);
            if (set == null || set == Array.Empty<char>()) return string.Empty;
            return set.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public string ToString(string format, IFormatProvider formatProvider) { return ToString(); }

        #endregion
    }
}