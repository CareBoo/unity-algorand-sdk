using System;

namespace AlgoSdk
{
    public class SerializationException : Exception
    {
        public SerializationException(string message) : base(message) { }
        public SerializationException(string message, Exception cause)
            : base(message, cause)
        {
        }
    }
}
