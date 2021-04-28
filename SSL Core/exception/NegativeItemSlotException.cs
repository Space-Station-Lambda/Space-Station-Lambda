using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Stack d'item négatif
    /// </summary>
    public class NegativeItemSlotException : Exception
    {
        public NegativeItemSlotException() {}   
        public NegativeItemSlotException(string message) : base(message) { }
        public NegativeItemSlotException(string message, Exception inner) : base(message, inner) { }
        protected NegativeItemSlotException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}