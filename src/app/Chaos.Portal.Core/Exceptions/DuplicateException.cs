namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception indicating that an action is missing
    /// </summary>
    public class DuplicateException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateException"/> class.
        /// </summary>
        public DuplicateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DuplicateException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DuplicateException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DuplicateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
