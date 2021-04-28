using System;

namespace SSL_Core.exception
{
    /// <summary>
    /// Deux identifiants uniques sont égaux; i.e utilisation d'un Id d'Item déjà utilisé
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