namespace ChoyUtilities {
    
    public enum EControlScheme : byte {
        Keyboard,
        Gamepad,
        Touchscreen,
        XR,
        NotDefined = byte.MaxValue
    }
}