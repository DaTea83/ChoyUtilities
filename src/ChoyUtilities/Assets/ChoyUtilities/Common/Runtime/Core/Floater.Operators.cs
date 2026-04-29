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

using System;
using Unity.Mathematics;

namespace ChoyUtilities {

    public partial struct Floater {

        private const float EPSILON = 0.0001f;

        #region Operators

        public int FirstMatch(float value) {
            var index = -1;

            for (var i = 0; i < values.Length; i++) {
                if (!(math.abs(values[i] - value) < EPSILON)) continue;
                index = i;

                break;
            }

            return index;
        }

        #endregion

    }

}