using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Stack d'item négatif
    /// </summary>
    public class NegativeStackItemException : Exception
    {
        public NegativeStackItemException() {}   
        public NegativeStackItemException(string message) : base(message) { }
        public NegativeStackItemException(string message, Exception inner) : base(message, inner) { }
        protected NegativeStackItemException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}