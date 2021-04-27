using System;

namespace SSL_Core.exception
{
    public class NegativeItemSlotException : Exception
    {
        public NegativeItemSlotException() : base() {}   
        public NegativeItemSlotException(string message) : base(message) { }
        public NegativeItemSlotException(string message, Exception inner) : base(message, inner) { }
        protected NegativeItemSlotException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}