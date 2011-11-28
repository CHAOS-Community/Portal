using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class SessionDoesNotExist : System.Exception
    {
        public SessionDoesNotExist()
        {
        }

        public SessionDoesNotExist(string message) : base(message)
        {
        }

        public SessionDoesNotExist(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected SessionDoesNotExist(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
