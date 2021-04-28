using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Stack d'item dépassé
    /// </summary>
    public class OutOfStackItemStackException : Exception
    {
        public OutOfStackItemStackException() { }   
        public OutOfStackItemStackException(string message) : base(message) { }
        public OutOfStackItemStackException(string message, Exception inner) : base(message, inner) { }
        protected OutOfStackItemStackException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
