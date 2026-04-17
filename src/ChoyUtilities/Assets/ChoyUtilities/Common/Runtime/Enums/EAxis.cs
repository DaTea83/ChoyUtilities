using System;

namespace ChoyUtilities {

    [Flags]
    public enum EAxis : byte {

        None = 0,
        X = 1,
        Y = 1 << 1,
        Z = 1 << 2

    }

}