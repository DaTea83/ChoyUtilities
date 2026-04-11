using System;

namespace ChoyUtilities {
    public class SingletonException : Exception {
        public SingletonException(string message) : base(message) { }
    }
}