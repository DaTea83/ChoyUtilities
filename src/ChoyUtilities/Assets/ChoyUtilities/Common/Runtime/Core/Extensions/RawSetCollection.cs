// Copyright 2026 DaTea83
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

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