using System;
using System.Collections.Generic;

namespace ChoyUtilities {

    public static partial class HelperCollection {

        public static int FirstMatchUnmanaged<T>(this T[] set, T value)
            where T : unmanaged
        {
            for (var i = 0; i < set.Length; i++) {
                if (EqualityComparer<T>.Default.Equals(set[i], value)) {
                    return i;
                }
            }
            return -1;
        }

        public static (T[], bool) AddIfNotContainUnmanaged<T>(this T[] set, T value)
            where T : unmanaged
        {
            foreach (var t in set) {
                if (EqualityComparer<T>.Default.Equals(t, value)) {
                    return (set, false);
                }
            }

            var newSet = new T[set.Length + 1];
            Array.Copy(set, newSet, set.Length);
            newSet[set.Length] = value;
            return (newSet, true);
        }

        public static (T[], bool) RemoveIfContainUnmanaged<T>(this T[] set, T value)
            where T : unmanaged
        {
            var removeIndex = set.FirstMatchUnmanaged(value);

            if (removeIndex == -1) {
                return (set, false);
            }
            var newArray = new T[set.Length - 1];
            if (removeIndex > 0) {
                Array.Copy(set, 0, newArray, 0, removeIndex);
            }

            if (removeIndex < set.Length - 1) {
                Array.Copy(set, removeIndex + 1, newArray, removeIndex, set.Length - removeIndex - 1);
            }

            return (newArray, true);
        }

    }

}