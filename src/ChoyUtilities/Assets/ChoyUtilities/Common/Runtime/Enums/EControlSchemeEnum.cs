using System;

namespace ChoyUtilities {
    
    public enum EControlSchemeEnum : byte {
        Keyboard,
        Gamepad,
        Touchscreen,
        XR,
        NotDefined = byte.MaxValue
    }
}