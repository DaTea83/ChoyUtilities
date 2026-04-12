using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public partial struct RawSet<T> {
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> F2ToFloat(float2 value, Allocator allocator = Allocator.Persistent)
        {
            var set = new RawSet<float>(2, allocator);
            set._values[0] = value.x;
            set._values[1] = value.y;
            return set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> F3ToFloat(float3 value, Allocator allocator = Allocator.Persistent) {
            var set = new RawSet<float>(3, allocator);
            set._values[0] = value.x;
            set._values[1] = value.y;
            set._values[2] = value.z;
            return set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> F4ToFloat(float4 value, Allocator allocator = Allocator.Persistent) {
            var set = new RawSet<float>(4, allocator);
            set._values[0] = value.x;
            set._values[1] = value.y;
            set._values[2] = value.z;
            set._values[3] = value.w;
            return set;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<int> Int4ToInt(int4 value, Allocator allocator = Allocator.Persistent) {
            var set = new RawSet<int>(4, allocator);
            set._values[0] = value.x;
            set._values[1] = value.y;
            set._values[2] = value.z;
            set._values[3] = value.w;
            return set;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> Quaternion(quaternion value, Allocator allocator = Allocator.Persistent) {
            return F4ToFloat(value.value, allocator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> Color(Color value, Allocator allocator = Allocator.Persistent) {
            var set = new float[] {
                value.r, value.g, value.b, value.a
            };
            return new RawSet<float>(set, allocator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<char> FromString(string value, Allocator allocator = Allocator.Persistent) {
            var set = value.ToCharArray();
            return new RawSet<char>(set, allocator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float> Floater(Floater value, Allocator allocator = Allocator.Persistent) {
            return new RawSet<float>(value.values, allocator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RawSet<float3> Transform(Transform value, Allocator allocator = Allocator.Persistent) {
            float3 pos = value.position;
            var rot = math.Euler(value.rotation);
            float3 scale = value.lossyScale;

            var set = new[] { pos, rot, scale};
            return new RawSet<float3>(set, allocator);
        }
    }
}