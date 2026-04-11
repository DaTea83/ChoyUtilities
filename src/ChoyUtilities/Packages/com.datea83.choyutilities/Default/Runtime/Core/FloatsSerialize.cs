using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    [Serializable]
    [BurstCompile]
    public readonly struct FloatsSerialize : IEquatable<FloatsSerialize>, 
        IComparable, IComparable<FloatsSerialize>, IFormattable{
        
        public readonly float[] Values;

        #region Constructors
        
        public FloatsSerialize(float[] values) { Values = values; }
        public FloatsSerialize(int[] values) {
            var set = new float[values.Length];
            Array.Copy(values, set, values.Length);
            Values = set;
        }
        public FloatsSerialize(FloatsSerialize values) : this(values.Values) { }
        public FloatsSerialize(float value) : this(new[] { value }) { }
        public FloatsSerialize(float2 value) : this(new[] { value.x, value.y }) { }
        public FloatsSerialize(float3 value) : this(new[] { value.x, value.y, value.z }) { }
        public FloatsSerialize(float4 value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public FloatsSerialize(Color value) : this(new[] { value.r, value.g, value.b, value.a }) { }
        public FloatsSerialize(Quaternion value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public FloatsSerialize(quaternion value) {
            var v = value.value; 
            Values = new[] { v.x, v.y, v.z, v.w };
        }
        public FloatsSerialize(Vector2 value) : this(new[] { value.x, value.y }) { }
        public FloatsSerialize(Vector3 value) : this(new[] { value.x, value.y, value.z }) { }
        public FloatsSerialize(Vector4 value) : this(new[] { value.x, value.y, value.z, value.w }) { }
        public FloatsSerialize(int value) : this(new [] { value }) { }
        public FloatsSerialize(int2 value) : this(new [] { value.x, value.y }) { }
        public FloatsSerialize(int3 value) : this(new [] { value.x, value.y, value.z }) { }
        public FloatsSerialize(int4 value) : this(new [] { value.x, value.y, value.z, value.w }) { }
        public FloatsSerialize(List<float> value) : this(value.ToArray()) { }
        public FloatsSerialize(Stack<float> value) : this(value.ToArray()) { }
        public FloatsSerialize(Queue<float> value) : this(value.ToArray()) { }
        public FloatsSerialize(NativeArray<float> value) : this(value.ToArray()) { }
        public FloatsSerialize(NativeList<float> value) : this(value.ToArray(allocator: Allocator.Temp)){}
        public FloatsSerialize(byte value) : this(new int[] { value }) { }
        public FloatsSerialize(byte[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(sbyte value) : this(new int[] { value }) { }
        public FloatsSerialize(sbyte[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(ushort value) : this(new int[] { value }) { }
        public FloatsSerialize(ushort[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(short value) : this(new int[] { value }) { }
        public FloatsSerialize(short[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(bool value) { Values = new[] { value ? 1f : 0f };}
        public FloatsSerialize(bool2 value) { Values = new[] { value.x ? 1f : 0f, value.y ? 1f : 0f };}
        public FloatsSerialize(bool[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) {
                set[i] = value[i] ? 1f : 0f;
            }
            Values = set;
        }
        //Lmao
        public FloatsSerialize(float4x4 value) {
            Values = new float[16];
            var c0 = value.c0; var c1 = value.c1; var c2 = value.c2; var c3 = value.c3;
            Values[0] = c0.x; Values[1] = c0.y; Values[2] = c0.z; Values[3] = c0.w;
            Values[4] = c1.x; Values[5] = c1.y; Values[6] = c1.z; Values[7] = c1.w;
            Values[8] = c2.x; Values[9] = c2.y; Values[10] = c2.z; Values[11] = c2.w;
            Values[12] = c3.x; Values[13] = c3.y; Values[14] = c3.z; Values[15] = c3.w;
        }
        public FloatsSerialize(Matrix4x4 value) {
            Values = new float[16];
            var c0 = value.GetColumn(0); var c1 = value.GetColumn(1); var c2 = value.GetColumn(2); var c3 = value.GetColumn(3);
            Values[0] = c0.x; Values[1] = c0.y; Values[2] = c0.z; Values[3] = c0.w;
            Values[4] = c1.x; Values[5] = c1.y; Values[6] = c1.z; Values[7] = c1.w;
            Values[8] = c2.x; Values[9] = c2.y; Values[10] = c2.z; Values[11] = c2.w;
            Values[12] = c3.x; Values[13] = c3.y; Values[14] = c3.z; Values[15] = c3.w;
        }
        public FloatsSerialize(double value) : this(new[] { (float) value }) { }
        public FloatsSerialize(double[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(half value) : this(new[] { (float) value }) { }
        public FloatsSerialize(half[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            Values = set;
        }
        public FloatsSerialize(Enum value) : this(new[] { (float) Convert.ChangeType(value, typeof(uint)) }) { }
        public FloatsSerialize(Enum[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) {
                set[i] = (float) Convert.ChangeType(value[i], typeof(uint));
            }
            Values = set;
        }
        
        #endregion
        
        #region Casting

        public static implicit operator int[](FloatsSerialize value) {
            var set = new int[value.Values.Length];
            Array.Copy(value.Values, set, value.Values.Length);
            return set;
        }
        
        #endregion
        
        #region Operators
        
        public float[] Add(float value) {
            return Values;
        }

        public float[] Add(float[] values) {
            return Values;
        }

        public float[] Add(FloatsSerialize values) {
            return Values;
        }
        
        public float[] Remove(float value, bool removeAllEqual = false) {
            return Values;
        }

        public float[] Remove(float[] values, bool removeAllEqual = false) {
            return Values;
        }

        public float[] Remove(FloatsSerialize values, bool removeAllEqual = false) {
            return Values;
        }

        #endregion
        
        #region IEquatable

        public bool Equals(FloatsSerialize other) { return Equals(Values, other.Values); }
        public override bool Equals(object obj) { return obj is FloatsSerialize other && Equals(other); }
        public override int GetHashCode() { return (Values is not null ? Values.GetHashCode() : 0); }

        #endregion

        #region IComparable

        public int CompareTo(FloatsSerialize other) { throw new NotImplementedException(); }
        public int CompareTo(object obj) { throw new NotImplementedException(); }

        #endregion

        #region IFormattable

        public override string ToString() {
            return $"{nameof(Values)}: {Values}";
        }
        public string ToString(string format, IFormatProvider formatProvider) {
            throw new NotImplementedException();
        }

        #endregion
    }
}