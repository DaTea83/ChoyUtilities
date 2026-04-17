using Unity.Mathematics;
using UnityEngine;

namespace ChoyUtilities {

    public static partial class HelperCollection {

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