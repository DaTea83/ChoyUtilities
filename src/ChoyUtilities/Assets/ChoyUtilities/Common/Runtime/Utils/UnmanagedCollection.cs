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
    public static partial class HelperCollection {
        public static int FirstMatchUnmanaged<T>(this T[] set, T value)
            where T : unmanaged {
            for (var i = 0; i < set.Length; i++)
                if (EqualityComparer<T>.Default.Equals(set[i], value))
                    return i;

            return -1;
        }

        public static (T[], bool) AddIfNotContainUnmanaged<T>(this T[] set, T value)
            where T : unmanaged {
            foreach (var t in set)
                if (EqualityComparer<T>.Default.Equals(t, value))
                    return (set, false);

            var newSet = new T[set.Length + 1];
            Array.Copy(set, newSet, set.Length);
            newSet[set.Length] = value;

            return (newSet, true);
        }

        public static (T[], bool) RemoveIfContainUnmanaged<T>(this T[] set, T value)
            where T : unmanaged {
            var removeIndex = set.FirstMatchUnmanaged(value);

            if (removeIndex == -1) return (set, false);
            var newArray = new T[set.Length - 1];
            if (removeIndex > 0) Array.Copy(set, 0, newArray, 0, removeIndex);

            if (removeIndex < set.Length - 1)
                Array.Copy(set, removeIndex + 1, newArray, removeIndex, set.Length - removeIndex - 1);

            return (newArray, true);
        }
    }
}