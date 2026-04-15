using System;
using Unity.Burst;

namespace ChoyUtilities {

    public static partial class HelperCollection {

        [BurstCompile]
        public static int FirstMatchUnmanaged<T>(this T[] set, T value)
            where T : unmanaged{
            var index = -1;
            for (var i = 0; i < set.Length; i++) {
                if (!set[i].Equals(value)) continue;
                index = i;
                break;
            }
            return index;
        }
        
        [BurstCompile]
        public static (T[], bool) AddIfNotContainUnmanaged<T>(this T[] set, T value)
            where T : unmanaged {
            foreach (var e in set) {
                if (e.Equals(value)) 
                    return (set, false);
                break;
            }
            var newSet = new T[set.Length + 1];
            set.CopyTo(newSet, 0);
            newSet[^1] = value;
            return (newSet, true);
        }

        [BurstCompile]
        public static (T[], bool) RemoveIfContainUnmanaged<T>(this T[] set, T value) 
            where T : unmanaged {
            var removeIndex = set.FirstMatchUnmanaged(value);
            if (removeIndex < -1) return (set, false);
            var newArray = new T[set.Length - 1];
            if(removeIndex > 0)
                Array.Copy(set, 0, newArray, 0, removeIndex);
            if (removeIndex < set.Length - 1)
                Array.Copy(set, removeIndex + 1, newArray, removeIndex, set.Length - removeIndex - 1);
            
            return (newArray, true);
        }

    }

}