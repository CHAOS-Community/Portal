namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception indicating that an action is missing
    /// </summary>
    public class DuplicateViewException : DuplicateException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateViewException"/> class.
        /// </summary>
        public DuplicateViewException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateViewException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public DuplicateViewException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateViewException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public DuplicateViewException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateViewException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DuplicateViewException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
