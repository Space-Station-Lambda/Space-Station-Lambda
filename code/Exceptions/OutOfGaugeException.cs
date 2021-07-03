using System;

namespace SSL.Exceptions
{
    public class OutOfGaugeException : Exception
    {
        public OutOfGaugeException() { }   
        public OutOfGaugeException(string message) : base(message) { }
        public OutOfGaugeException(string message, Exception inner) : base(message, inner) { }
        protected OutOfGaugeException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}