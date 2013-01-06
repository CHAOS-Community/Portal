using System.Runtime.Serialization;

namespace Chaos.Portal.Exceptions
{
    public class InsufficientPermissionsException : System.Exception
    {
        public InsufficientPermissionsException()
        {
        }

        public InsufficientPermissionsException(string message)
            : base(message)
        {
        }

        public InsufficientPermissionsException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected InsufficientPermissionsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
