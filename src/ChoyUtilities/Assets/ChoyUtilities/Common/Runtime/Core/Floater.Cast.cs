using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public partial struct Floater {
        #region Casting

        public static implicit operator float(Floater value) => value.Values[0];

        public static implicit operator float[](Floater value) => value.Values;

        public static implicit operator List<float>(Floater value) => new (value.Values);

        public static implicit operator int[](Floater value) {
            if (value.Values == null || value.Values.Length == 0) throw new FloaterException("Empty Floater found");
            var set = new int[value.Values.Length];
            Array.Copy(value.Values, set, value.Values.Length);
            return set;
        }

        //public static implicit operator Array(FloatsSerialize value) => value.Values;
        public static implicit operator float2(Floater value) {
            if (value.Values is null || value.Values.Length < 2)
                throw new FloaterException("Value must be more than 2 floats");
            var set = new float2(value.Values[0], value.Values[1]);
            return set;
        }

        public static implicit operator float3(Floater value) {
            if (value.Values is null || value.Values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new float3(value.Values[0], value.Values[1], value.Values[2]);
            return set;
        }

        public static implicit operator float4(Floater value) {
            if (value.Values is null || value.Values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new float4(value.Values[0], value.Values[1], value.Values[2], value.Values[3]);
            return set;
        }

        public static implicit operator Vector3(Floater value) {
            if (value.Values is null || value.Values.Length < 3)
                throw new FloaterException("Value must be more than 3 floats");
            var set = new Vector3(value.Values[0], value.Values[1], value.Values[2]);
            return set;
        }

        public static implicit operator Quaternion(Floater value) {
            if (value.Values is null || value.Values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new Quaternion(value.Values[0], value.Values[1], value.Values[2], value.Values[3]);
            return set;
        }

        public static implicit operator quaternion(Floater value) {
            if (value.Values is null || value.Values.Length < 4)
                throw new FloaterException("Value must be more than 4 floats");
            var set = new quaternion(value.Values[0], value.Values[1], value.Values[2], value.Values[3]);
            return set;
        }

        public static implicit operator Color(Floater value) {
            if (value.Values is null || value.Values.Length < 4) return Color.white;
            var set = new Color(value.Values[0], value.Values[1], value.Values[2], value.Values[3]);
            return set;
        }

        public static implicit operator char[](Floater value) {
            if (value.Values == null || value.Values.Length == 0) return Array.Empty<char>();
            var set = new char[value.Values.Length];
            for (var i = 0; i < value.Values.Length; i++) {
                var j = (char)value.Values[i];
                set[i] = j;
            }

            return set;
        }
        public static implicit operator NativeArray<float>.ReadOnly(Floater value) {
            return new NativeArray<float>(value.Values, Allocator.Temp).AsReadOnly();
        }
        
        public static implicit operator RawSet<float>(Floater value) => new (value.Values, Allocator.Temp);

        #endregion
    }
}