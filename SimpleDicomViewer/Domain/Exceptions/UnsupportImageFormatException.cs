using System;

namespace SimpleDicomViewer.Domain.Exceptions
{
    public class UnsupportImageFormatException : Exception
    {
        public UnsupportImageFormatException() : base() { }

        public UnsupportImageFormatException(string message) : base(message) { }

        public UnsupportImageFormatException(string message, System.Exception innerException) : base(message, innerException) { }

        protected UnsupportImageFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
