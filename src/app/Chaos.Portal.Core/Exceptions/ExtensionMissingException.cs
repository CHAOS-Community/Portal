namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    public class ExtensionMissingException : System.Exception
    {
        public ExtensionMissingException()
        {
        }

        public ExtensionMissingException(string message)
            : base(message)
        {
        }

        public ExtensionMissingException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ExtensionMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
