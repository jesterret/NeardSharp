using System;
using System.Runtime.Serialization;

namespace NeardSharp
{
    /// <summary>
    /// Exception that is thrown when <see cref="NfcRecord.Type"/> has invalid value. 
    /// </summary>
    [Serializable]
    public class InvalidRecordTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidRecordTypeException"/>.
        /// </summary>
        public InvalidRecordTypeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidRecordTypeException"/> with specified error <paramref name="message"/>.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        public InvalidRecordTypeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidRecordTypeException"/> with specified error <paramref name="message"/> and a reference to the <paramref name="innerException"/> that is the cause of this exception.
        /// </summary>
        /// <param name="message">Message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of this exception, or a <see langword="null"/> reference if no inner exception is specified.</param>
        public InvalidRecordTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidRecordTypeException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="info"/> parameter is <see langword="null"/>.</exception>
        /// <exception cref="SerializationException">The class name is <see langword="null"/> or <see cref="Exception.HResult"/> is zero.</exception>
        protected InvalidRecordTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}