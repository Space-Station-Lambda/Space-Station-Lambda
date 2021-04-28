using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Stack d'item négatif
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