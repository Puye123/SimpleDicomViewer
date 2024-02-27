using System;

namespace SimpleDicomViewer.Domain.Exceptions
{
    /// <summary>
    /// DICOMのデータ要素のフォーマットに不正があった場合にthrowする例外
    /// </summary>
    [Serializable]
    public class InvalidDICOMFormatException : System.Exception
    {
        
        public InvalidDICOMFormatException() : base() { }

        public InvalidDICOMFormatException(string message) : base("DICOMの仕様に反したデータフォーマットです: " + message) { }

        public InvalidDICOMFormatException(string message, System.Exception innerException) : base("DICOMの仕様に反したデータフォーマットです: " + message, innerException) { }

        protected InvalidDICOMFormatException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
    }
}
