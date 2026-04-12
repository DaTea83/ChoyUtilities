using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {
    
    public static partial class HelperCollection {

        public static Transform Floater(this Transform obj, Floater data) {
            if (data.values.Length < 9) return obj;
            var euler = new Vector3(data.values[3], data.values[4], data.values[5]);
            obj.position = new Vector3(data.values[0], data.values[1], data.values[2]);
            obj.rotation = quaternion.Euler(euler);
            obj.localScale = new Vector3(data.values[6], data.values[7], data.values[8]);
            return obj;
        }
        
        public static Transform RawSet(this Transform obj, RawSet<float3> set) {
            if (!set.IsCreated || set.Length < 3) throw new RawSetException("Length of RawSet need to more than 3");
            var pos = set[0];
            var rot = quaternion.Euler(set[1]);
            var scale = set[2];
            obj.position = pos;
            obj.rotation = rot;
            obj.localScale = scale;
            return obj;
        }
    }
}