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
using System.Collections.Generic;

namespace ChoyUtilities {
    public static class EnumCollection {
        public static bool IsSingleFlag<T>(this T flag)
            where T : struct, Enum {
            if (!typeof(T).IsDefined(typeof(FlagsAttribute), false))
                throw new ArgumentException($"{flag} must have [FlagAttribute].", nameof(flag));

            var val = Convert.ToUInt64(flag);

            if (val == 0) return true;

            //Check if the value is a single flag
            return (val & (val - 1)) == 0;
        }

        public static T GetHighestFlag<T>(this T value)
            where T : struct, Enum {
            var inputValue = Convert.ToUInt64(value);
            ulong highestValue = 0;

            foreach (T e in Enum.GetValues(typeof(T))) {
                var currentValue = Convert.ToUInt64(e);

                if (currentValue == 0) continue;

                if ((inputValue & currentValue) == currentValue && currentValue > highestValue)
                    highestValue = currentValue;
            }

            return (T)Enum.ToObject(typeof(T), highestValue);
        }
    }
}