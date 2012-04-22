using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
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
