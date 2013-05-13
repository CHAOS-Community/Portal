namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    public class ProtocolVersionException : System.Exception
    {
        public ProtocolVersionException()
        {
        }

        public ProtocolVersionException(string message)
            : base(message)
        {
        }

        public ProtocolVersionException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ProtocolVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
