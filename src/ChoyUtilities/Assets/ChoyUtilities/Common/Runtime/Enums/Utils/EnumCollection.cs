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
        
        /// <summary>
        /// Is more recommended to use <see cref="HelperCollection.FirstMatchUnmanaged"/>.
        /// Due to legacy reasons, this stays
        /// </summary>
        public static int FirstMatch<T>(this T[] set, T value)
            where T : struct, Enum {
            for (var i = 0; i < set.Length; i++)
                if (EqualityComparer<T>.Default.Equals(set[i], value))
                    return i;

            return -1;
        }

        /// <summary>
        /// Is more recommended to use <see cref="HelperCollection.AddIfNotContainUnmanaged"/>.
        /// Due to legacy reasons, this stays
        /// </summary>
        public static (T[], bool) AddIfNotContain<T>(this T[] set, T value)
            where T : struct, Enum {
            foreach (var t in set)
                if (EqualityComparer<T>.Default.Equals(t, value))
                    return (set, false);

            var newSet = new T[set.Length + 1];
            Array.Copy(set, newSet, set.Length);
            newSet[set.Length] = value;

            return (newSet, true);
        }

        /// <summary>
        /// Is more recommended to use <see cref="HelperCollection.RemoveIfContainUnmanaged"/>.
        /// Due to legacy reasons, this stays
        /// </summary>
        public static (T[], bool) RemoveIfContain<T>(this T[] set, T value)
            where T : struct, Enum {
            var removeIndex = set.FirstMatch(value);

            if (removeIndex == -1) return (set, false);
            var newArray = new T[set.Length - 1];
            if (removeIndex > 0) Array.Copy(set, 0, newArray, 0, removeIndex);

            if (removeIndex < set.Length - 1)
                Array.Copy(set, removeIndex + 1, newArray, removeIndex, set.Length - removeIndex - 1);

            return (newArray, true);
        }
    }
}