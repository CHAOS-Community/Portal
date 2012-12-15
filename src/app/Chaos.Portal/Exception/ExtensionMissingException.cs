using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
{
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
