using System;

namespace ChoyUtilities {
    
    public static partial class HelperCollection {
        
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
        
        public static int FirstMatch<T>(this T[] set, T value)
            where T : struct, Enum
        {
            for (var i = 0; i < set.Length; i++) {
                if (set[i].Equals(value)) {
                    return i;
                }
            }
            return -1;
        }
        
        public static (T[], bool) AddIfNotContain<T>(this T[] set, T value)
            where T : struct, Enum {
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

        public static (T[], bool) RemoveIfContain<T>(this T[] set, T value) 
            where T : struct, Enum {
            var removeIndex = set.FirstMatch(value);
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