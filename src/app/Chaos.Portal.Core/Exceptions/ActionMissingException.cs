namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    /// <summary>
    /// An exception indicating that an action is missing
    /// </summary>
    public class ActionMissingException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMissingException"/> class.
        /// </summary>
        public ActionMissingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMissingException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ActionMissingException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMissingException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="innerException">
        /// The inner exception.
        /// </param>
        public ActionMissingException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMissingException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ActionMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
