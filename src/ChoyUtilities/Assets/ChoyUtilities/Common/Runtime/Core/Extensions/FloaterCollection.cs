using System;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public static partial class HelperCollection {

        public static float3 PositionFromTransform(this Floater data) {
            return data.values.Length < 9 ? float3.zero : new float3(data.values[0], data.values[1], data.values[2]);
        }
        
        public static quaternion RotationFromTransform(this Floater data) {
            if (data.values.Length < 9) return quaternion.identity;
            var euler = new Vector3(data.values[3], data.values[4], data.values[5]);
            return quaternion.Euler(euler);
        }
        
        public static float3 ScaleFromTransform(this Floater data) {
            return data.values.Length < 9 ? new float3(1, 1, 1) : new float3(data.values[6], data.values[7], data.values[8]);
        }
        
        public static Transform Floater(this Transform obj, Floater data) {
            if (data.values.Length < 9) return obj;
            var euler = new Vector3(data.values[3], data.values[4], data.values[5]);
            obj.position = new Vector3(data.values[0], data.values[1], data.values[2]);
            obj.rotation = quaternion.Euler(euler);
            obj.localScale = new Vector3(data.values[6], data.values[7], data.values[8]);
            return obj;
        }

        public static Floater Floater<T>(this T value) 
            where T : struct, Enum{
            var i = (Convert.ToUInt32(value));
            return new Floater(i);
        }
        
        public static Floater Floater<T>(this T[] value) 
            where T : struct, Enum{
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) 
                set[i] = Convert.ToUInt32(value[i]);
            return new Floater(set);
        }
        
        public static T GetEnum<T>(this Floater value) 
            where T : struct, Enum{
            return (T)Convert.ChangeType((uint)value.values[0], typeof(T));
        }

        public static T[] GetEnums<T>(this Floater value) 
        where T : struct, Enum {
            var ts = new T[value.values.Length];
            for (var i = 0; i < value.values.Length; i++) {
                ts[i] = (T)Convert.ChangeType((uint)value.values[i], typeof(T));
            }
            return ts;
        }
    }
}