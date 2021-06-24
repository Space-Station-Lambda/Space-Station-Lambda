using System;

namespace SSL_Core.item
{
    /// <summary>
    /// Thrown when an action could not be executed because of a full inventory.
    /// </summary>
    ///
    /// Example: Adding an item in an inventory with no space left.
    public class FullInventoryException : Exception
    {
        public FullInventoryException() {}   
        public FullInventoryException(string message) : base(message) { }
        public FullInventoryException(string message, Exception inner) : base(message, inner) { }
        protected FullInventoryException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}