using System;

namespace SSL_Core.exception
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() {}   
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ItemNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}