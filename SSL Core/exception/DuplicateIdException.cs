using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Two identical IDs are equals; i.e use of an item ID already used
    /// </summary>
    public class DuplicateIdException : Exception
    {
        public DuplicateIdException() {}   
        public DuplicateIdException(string message) : base(message) { }
        public DuplicateIdException(string message, Exception inner) : base(message, inner) { }
        protected DuplicateIdException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}