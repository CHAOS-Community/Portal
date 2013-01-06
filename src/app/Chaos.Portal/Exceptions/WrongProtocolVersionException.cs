using System.Runtime.Serialization;

namespace Chaos.Portal.Exceptions
{
    public class WrongProtocolVersionException : System.Exception
    {
        public WrongProtocolVersionException()
        {
        }

        public WrongProtocolVersionException(string message)
            : base(message)
        {
        }

        public WrongProtocolVersionException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected WrongProtocolVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
