using System;

namespace ChoyUtilities {

    public sealed class SingletonException : Exception {

        public SingletonException(string message) : base(message) { }

    }

}