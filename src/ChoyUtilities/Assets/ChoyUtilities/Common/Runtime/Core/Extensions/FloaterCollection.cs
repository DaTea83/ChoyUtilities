using System;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public static partial class HelperCollection {

        [BurstCompile]
        public static float3 PositionFromTransform(this Floater data) {
            return data.Length < 9 ? float3.zero : new float3(data[0], data[1], data[2]);
        }
        
        [BurstCompile]
        public static quaternion RotationFromTransform(this Floater data) {
            if (data.Length < 9) return quaternion.identity;
            var euler = new Vector3(data[3], data[4], data[5]);
            return quaternion.Euler(euler);
        }
        
        [BurstCompile]
        public static float3 ScaleFromTransform(this Floater data) {
            return data.Length < 9 ? new float3(1, 1, 1) : new float3(data[6], data[7], data[8]);
        }
        
        public static Transform Floater(this Transform obj, Floater data) {
            if (data.Length < 9) return obj;
            var euler = new Vector3(data[3], data[4], data[5]);
            obj.position = new Vector3(data[0], data[1], data[2]);
            obj.rotation = quaternion.Euler(euler);
            obj.localScale = new Vector3(data[6], data[7], data[8]);
            return obj;
        }

        public static Floater Floater<T>(this T value) 
            where T : struct, Enum{
            var i = (Convert.ToSingle(value));
            return new Floater(i);
        }
        
        public static Floater Floater<T>(this T[] value) 
            where T : struct, Enum{
            var set = new float[value.Length];
            for (var i = 0; i < value.Length; i++) 
                set[i] = Convert.ToSingle(value[i]);
            return new Floater(set);
        }
        
        public static T GetEnum<T>(this Floater value) 
            where T : struct, Enum{
            return (T)Enum.ToObject(typeof(T), (uint)value[0]);
        }

        public static T[] GetEnums<T>(this Floater value) 
        where T : struct, Enum {
            if (!value.IsCreated)
                throw new FloaterException("Floater is not created yet.");
            
            var results = new T[value.Length];
            for (var i = 0; i < value.Length; i++) {
                results[i] = (T)Enum.ToObject(typeof(T), (uint)value[i]);
            }
            return results;
        }
    }
}