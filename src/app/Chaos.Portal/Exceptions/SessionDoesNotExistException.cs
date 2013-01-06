using System.Runtime.Serialization;

namespace Chaos.Portal.Exceptions
{
    public class SessionDoesNotExistException : System.Exception
    {
        public SessionDoesNotExistException()
        {
        }

        public SessionDoesNotExistException(string message)
            : base(message)
        {
        }

        public SessionDoesNotExistException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected SessionDoesNotExistException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
