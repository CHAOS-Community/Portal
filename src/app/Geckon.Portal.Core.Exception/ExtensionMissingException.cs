using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class ExtensionMissingException : System.Exception
    {
        public ExtensionMissingException()
        {
        }

        public ExtensionMissingException(string message) : base(message)
        {
        }

        public ExtensionMissingException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected ExtensionMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
