using System;

namespace SSL.Exceptions
{
    /// <summary>
    /// Negative item stack
    /// </summary>
    public class NegativeItemStackException : Exception
    {
        public NegativeItemStackException() {}   
        public NegativeItemStackException(string message) : base(message) { }
        public NegativeItemStackException(string message, Exception inner) : base(message, inner) { }
        protected NegativeItemStackException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}