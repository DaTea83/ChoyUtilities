using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public partial struct Floater {
        #region Casting

        public static implicit operator float(Floater value) => value._values[0];

        public static implicit operator float[](Floater value) => value._values;

        public static implicit operator List<float>(Floater value) => new (value._values);

        public static implicit operator int[](Floater value) {
            if (value._values == null || value._values.Length == 0) throw new FloaterException("Empty Floater found");
            var set = new int[value._values.Length];
            Array.Copy(value._values, set, value._values.Length);
            return set;
        }

        //public static implicit operator Array(FloatsSerialize value) => value.Values;
        public static implicit operator float2(Floater value) {
            if (value._values is null || value._values.Length < 2)
                throw new FloaterException("Value must be more than 2 floats");
            var set = new float2(value._values[0], value._values[1]);
            return set;
        }

        public static implicit operator float3(Floater value) {
            if (value._values is null || value._values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new float3(value._values[0], value._values[1], value._values[2]);
            return set;
        }

        public static implicit operator float4(Floater value) {
            if (value._values is null || value._values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new float4(value._values[0], value._values[1], value._values[2], value._values[3]);
            return set;
        }

        public static implicit operator Vector3(Floater value) {
            if (value._values is null || value._values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new Vector3(value._values[0], value._values[1], value._values[2]);
            return set;
        }

        public static implicit operator Quaternion(Floater value) {
            if (value._values is null || value._values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new Quaternion(value._values[0], value._values[1], value._values[2], value._values[3]);
            return set;
        }

        public static implicit operator quaternion(Floater value) {
            if (value._values is null || value._values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new quaternion(value._values[0], value._values[1], value._values[2], value._values[3]);
            return set;
        }

        public static implicit operator Color(Floater value) {
            if (value._values is null || value._values.Length < 4) return Color.white;
            var set = new Color(value._values[0], value._values[1], value._values[2], value._values[3]);
            return set;
        }

        public static implicit operator char[](Floater value) {
            if (value._values == null || value._values.Length == 0) return Array.Empty<char>();
            var set = new char[value._values.Length];
            for (var i = 0; i < value._values.Length; i++) {
                var j = (char)value._values[i];
                set[i] = j;
            }

            return set;
        }
        public static implicit operator NativeArray<float>.ReadOnly(Floater value) {
            return new NativeArray<float>(value._values, Allocator.Temp).AsReadOnly();
        }
        
        public static implicit operator RawSet<float>(Floater value) => new (value._values, Allocator.Temp);

        #endregion
    }
}