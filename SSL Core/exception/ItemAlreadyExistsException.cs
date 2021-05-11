using System;

namespace SSL_Core.exception
{
    public class ItemAlreadyExistsException : Exception
    {
        public ItemAlreadyExistsException() {}   
        public ItemAlreadyExistsException(string message) : base(message) { }
        public ItemAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
        protected ItemAlreadyExistsException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}