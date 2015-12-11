namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception indicating that an action is missing
    /// </summary>
    public class DuplicateEndpointException : DuplicateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEndpointException"/> class.
        /// </summary>
        public DuplicateEndpointException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEndpointException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DuplicateEndpointException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEndpointException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DuplicateEndpointException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateEndpointException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DuplicateEndpointException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
