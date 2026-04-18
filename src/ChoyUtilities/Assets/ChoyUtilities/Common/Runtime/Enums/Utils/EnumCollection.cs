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