using System.Collections.Generic;
using Unity.Collections;

namespace ChoyUtilities {

    public partial struct RawSet<T> {

        public static implicit operator NativeArray<T>(RawSet<T> value) {
            return value._values;
        }

        public static implicit operator NativeArray<T>.ReadOnly(RawSet<T> value) {
            return value._values.AsReadOnly();
        }

        public static implicit operator T[](RawSet<T> value) {
            return value._values.ToArray();
        }

        public static implicit operator List<T>(RawSet<T> value) {
            var l = new List<T>();
            foreach (var t in value._values) l.Add(t);

            return l;
        }

    }

}