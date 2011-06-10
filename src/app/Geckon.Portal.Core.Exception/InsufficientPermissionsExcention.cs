using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class InsufficientPermissionsExcention :System.Exception
    {
        public InsufficientPermissionsExcention()
        {
        }

        public InsufficientPermissionsExcention(string message) : base(message)
        {
        }

        public InsufficientPermissionsExcention(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InsufficientPermissionsExcention(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
