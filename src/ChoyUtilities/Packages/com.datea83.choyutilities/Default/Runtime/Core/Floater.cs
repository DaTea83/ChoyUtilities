using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    [Serializable]
    [BurstCompile]
    public struct Floater : IEquatable<Floater>,
        IComparable, IComparable<Floater>, IFormattable{
        
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
        public Floater(int value) : this(new [] { value }) { }
        public Floater(int2 value) : this(new [] { value.x, value.y }) { }
        public Floater(int3 value) : this(new [] { value.x, value.y, value.z }) { }
        public Floater(int4 value) : this(new [] { value.x, value.y, value.z, value.w }) { }
        public Floater(List<float> value) : this(value.ToArray()) { }
        public Floater(Stack<float> value) : this(value.ToArray()) { }
        public Floater(Queue<float> value) : this(value.ToArray()) { }
        public Floater(NativeArray<float> value) : this(value.ToArray()) { }
        public Floater(NativeList<float> value) : this(value.ToArray(allocator: Allocator.Temp)){}
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
        public Floater(bool value) { values = new[] { value ? 1f : 0f };}
        public Floater(bool2 value) { values = new[] { value.x ? 1f : 0f, value.y ? 1f : 0f };}
        public Floater(bool[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) {
                set[i] = value[i] ? 1f : 0f;
            }
            values = set;
        }
        public Floater(float4x4 value) {
            values = new float[16];
            var c0 = value.c0; var c1 = value.c1; var c2 = value.c2; var c3 = value.c3;
            values[0] = c0.x; values[1] = c0.y; values[2] = c0.z; values[3] = c0.w;
            values[4] = c1.x; values[5] = c1.y; values[6] = c1.z; values[7] = c1.w;
            values[8] = c2.x; values[9] = c2.y; values[10] = c2.z; values[11] = c2.w;
            values[12] = c3.x; values[13] = c3.y; values[14] = c3.z; values[15] = c3.w;
        }
        public Floater(Matrix4x4 value) {
            values = new float[16];
            var c0 = value.GetColumn(0); var c1 = value.GetColumn(1); var c2 = value.GetColumn(2); var c3 = value.GetColumn(3);
            values[0] = c0.x; values[1] = c0.y; values[2] = c0.z; values[3] = c0.w;
            values[4] = c1.x; values[5] = c1.y; values[6] = c1.z; values[7] = c1.w;
            values[8] = c2.x; values[9] = c2.y; values[10] = c2.z; values[11] = c2.w;
            values[12] = c3.x; values[13] = c3.y; values[14] = c3.z; values[15] = c3.w;
        }
        public Floater(double value) : this(new[] { (float) value }) { }
        public Floater(double[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(half value) : this(new[] { (float) value }) { }
        public Floater(half[] value) {
            var set = new float[value.Length];
            Array.Copy(value, set, value.Length);
            values = set;
        }
        public Floater(Enum value) : this(new[] { (int) Convert.ChangeType(value, typeof(int)) }) { }
        public Floater(Enum[] value) {
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) {
                set[i] = (int) Convert.ChangeType(value[i], typeof(int));
            }
            values = set;
        }
        public Floater(char value) : this(new[] { (float) value }) { }
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
        /// 
        /// </summary>
        /// <param name="value">
        /// For Transform, it will convert quaternion to euler angles before storing.
        /// To convert FloatsSerialize back to Transform use <see cref="HelperCollection.FloatsSerializeToTransform"/> extension
        /// </param>
        public Floater(Transform value) {
            var set = new float[9];
            var euler = math.Euler(value.rotation);
            set[0] = value.position.x; set[1] = value.position.y; set[2] = value.position.z;
            set[3] = euler.x; set[4] = euler.y; set[5] = euler.z;
            set[6] = value.localScale.x; set[7] = value.localScale.y; set[8] = value.localScale.z;
            values = set;
        }
        
        #endregion
        
        #region Casting

        public static implicit operator float(Floater value) => value.values[0];
        public static implicit operator float[](Floater value) => value.values;
        public static implicit operator List<float>(Floater value) => new (value.values);
        public static implicit operator int[](Floater value) {
            if (value.values == null || value.values.Length == 0) throw new FloaterException("Empty Floater found");
            var set = new int[value.values.Length];
            Array.Copy(value.values, set, value.values.Length);
            return set;
        }

        public static implicit operator Enum(Floater value) {
            if (value.values == null || value.values.Length == 0) throw new FloaterException("Empty Floater found");
            return (Enum) Convert.ChangeType(value.values[0], typeof(Enum));
        }
        public static implicit operator Enum[](Floater value) {
            if (value.values == null || value.values.Length == 0) return Array.Empty<Enum>();
            var set = new Enum[value.values.Length];
            for (var i = 0; i < value.values.Length; i++) {
                set[i] = (Enum) Convert.ChangeType(value.values[i], set[i].GetType());
            }
            return set;
        }
        //public static implicit operator Array(FloatsSerialize value) => value.Values;
        public static implicit operator float2(Floater value) {
            if (value.values is null || value.values.Length < 2) throw new FloaterException("Value must be more than 2 floats");
            var set = new float2(value.values[0], value.values[1]);
            return set;
        }
        public static implicit operator float3(Floater value) {
            if (value.values is null || value.values.Length < 3) throw new FloaterException("Value must be more than 3 floats");
            var set = new float3(value.values[0], value.values[1], value.values[2]);
            return set;
        }
        public static implicit operator float4(Floater value) {
            if (value.values is null || value.values.Length < 4) throw new FloaterException("Value must be more than 4 floats");
            var set = new float4(value.values[0], value.values[1], value.values[2], value.values[3]);
            return set;
        }
        public static implicit operator Vector3(Floater value) {
            if (value.values is null || value.values.Length < 3) throw new FloaterException("Value must be more than 3 floats");
            var set = new Vector3(value.values[0], value.values[1], value.values[2]);
            return set;
        }
        public static implicit operator Quaternion(Floater value) {
            if (value.values is null || value.values.Length < 4) throw new FloaterException("Value must be more than 4 floats");
            var set = new Quaternion(value.values[0], value.values[1], value.values[2], value.values[3]);
            return set;
        }
        public static implicit operator quaternion(Floater value) {
            if (value.values is null || value.values.Length < 4) throw new FloaterException("Value must be more than 4 floats");
            var set = new quaternion(value.values[0], value.values[1], value.values[2], value.values[3]);
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
        public static implicit operator NativeArray<float>(Floater value) => new NativeArray<float>(value.values, Allocator.Temp);
        
        #endregion
        
        #region Operators
        
        public Floater Add(float value) {
            var set = new float[values.Length + 1];
            if (set == null) throw new FloaterException(nameof(set));
            Array.Copy(values, set, values.Length);
            set[^1] = value;
            
            return new Floater(set);
        }

        public Floater Add(float[] extras) {
            if (extras == null || extras == Array.Empty<float>()) return this;
            var set = new float[values.Length + extras.Length];
            if (set == null) throw new FloaterException(nameof(set));
            
            for(var i = 0; i < values.Length; i++)
                set[i] = values[i];
            for(var i = 0; i < extras.Length; i++)
                set[i + values.Length] = extras[i];
            
            return new Floater(set);
        }

        public Floater Add(Floater extra) {
            if (extra.values == null || extra.values == Array.Empty<float>()) return this;
            var set = new float[values.Length + extra.values.Length];
            if (set == null) throw new FloaterException(nameof(set));
            
            for(var i = 0; i < values.Length; i++)
                set[i] = values[i];
            for(var i = 0; i < extra.values.Length; i++)
                set[i + values.Length] = extra.values[i];
            
            return new Floater(set);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        /// If true, will remove all values that are equal to <paramref name="value"/>,
        /// otherwise will remove only the first value that is equal to <paramref name="value"/>.
        /// </param>
        /// <returns></returns>
        public Floater Remove(float value, bool removeAllEqual = false) {
            if (values == null || values.Length == 0) return new Floater(Array.Empty<float>());
            const float epsilon = 0.0001f;

            if (removeAllEqual) {
                var keepCount = 0;

                foreach (var t in values) {
                    if (math.abs(t - value) >= epsilon)
                        keepCount++;
                }

                if (keepCount == values.Length) return this;
                if (keepCount == 0) return new Floater(Array.Empty<float>());

                var set = new float[keepCount];
                var j = 0;

                foreach (var t in values) {
                    if (math.abs(t - value) < epsilon) continue;
                    set[j] = t;
                    j++;
                }

                return new Floater(set);
            }
            else {
                var removeIndex = -1;

                for (var i = 0; i < values.Length; i++) {
                    if (!(math.abs(values[i] - value) < epsilon)) continue;
                    removeIndex = i;
                    break;
                }

                if (removeIndex < 0) return this;
                if (values.Length == 1) return new Floater(Array.Empty<float>());
                var set = new float[values.Length - 1];

                if (removeIndex > 0) 
                    Array.Copy(values, 0, set, 0, removeIndex);
                if (removeIndex < values.Length - 1)
                    Array.Copy(values, removeIndex + 1, set, removeIndex, values.Length - removeIndex - 1);

                return new Floater(set);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        /// If true, will remove all values that <paramref name="value"/> contains,
        /// otherwise will remove only the first value that in <paramref name="value"/>.
        /// </param>
        /// <returns></returns>
        public Floater Remove(float[] value, bool removeAllEqual = false) {
            return removeAllEqual ? RemoveAllMatches(value) : RemoveFirstMatches(value);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">
        /// Value to remove.
        /// </param>
        /// <param name="removeAllEqual">
        /// If true, will remove all values that <paramref name="value"/> contains,
        /// otherwise will remove only the first value that in <paramref name="value"/>.
        /// </param>
        /// <returns></returns>
        public Floater Remove(Floater value, bool removeAllEqual = false) {
            return removeAllEqual ? RemoveAllMatches(value.values) : RemoveFirstMatches(value.values);
        }
        
        private Floater RemoveFirstMatches(float[] valuesToRemove) {
            if (values == null || values.Length == 0) return new Floater(Array.Empty<float>());
            if (valuesToRemove == null || valuesToRemove.Length == 0) return this;

            const float epsilon = 0.0001f;
            var consumed = new bool[valuesToRemove.Length];
            var keepCount = 0;

            foreach (var t in values) {
                var shouldRemove = false;

                for (var j = 0; j < valuesToRemove.Length; j++) {
                    if (consumed[j]) continue;

                    if (!(math.abs(t - valuesToRemove[j]) < epsilon)) continue;
                    consumed[j] = true;
                    shouldRemove = true;
                    break;
                }

                if (!shouldRemove) keepCount++;
            }

            if (keepCount == values.Length) return this;
            if (keepCount == 0) return new Floater(Array.Empty<float>());

            var set = new float[keepCount];
            consumed = new bool[valuesToRemove.Length];
            var index = 0;

            foreach (var t in values) {
                var shouldRemove = false;

                for (var j = 0; j < valuesToRemove.Length; j++) {
                    if (consumed[j]) continue;

                    if (!(math.abs(t - valuesToRemove[j]) < epsilon)) continue;
                    consumed[j] = true;
                    shouldRemove = true;
                    break;
                }
                if (shouldRemove) continue;

                set[index] = t;
                index++;
            }
            return new Floater(set);
        }
        
        private Floater RemoveAllMatches(float[] valuesToRemove)
        {
            if (values == null || values.Length == 0) return new Floater(Array.Empty<float>());
            if (valuesToRemove == null || valuesToRemove.Length == 0) return this;
            const float epsilon = 0.0001f;
            var keepCount = 0;

            foreach (var t in values) {
                var shouldRemove = false;
                foreach (var t1 in valuesToRemove) {
                    if (!(math.abs(t - t1) < epsilon)) continue;
                    shouldRemove = true;
                    break;
                }

                if (!shouldRemove) keepCount++;
            }

            if (keepCount == values.Length) return this;
            if (keepCount == 0) return new Floater(Array.Empty<float>());

            var set = new float[keepCount];
            var index = 0;

            foreach (var t in values) {
                var shouldRemove = false;
                foreach (var t1 in valuesToRemove) {
                    if (!(math.abs(t - t1) < epsilon)) continue;
                    shouldRemove = true;
                    break;
                }

                if (shouldRemove)
                    continue;

                set[index] = t;
                index++;
            }
            return new Floater(set);
        }

        #endregion
        
        #region IEquatable

        public bool Equals(Floater other) { return Equals(values, other.values); }
        public override bool Equals(object obj) { return obj is Floater other && Equals(other); }
        public override int GetHashCode() { return (values is not null ? values.GetHashCode() : 0); }

        #endregion

        #region IComparable

        public int CompareTo(Floater other) {
            float selfV = 0;
            float otherV = 0;
            foreach (var f in values) { selfV += f; }
            foreach (var f in other.values) { otherV += f; }
            
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

        public string ToString(string format, IFormatProvider formatProvider) {
            return ToString();
        }

        #endregion
    }
}