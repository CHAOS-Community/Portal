using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
{
    public class ActionMissingException : System.Exception
    {
        public ActionMissingException()
        {
        }

        public ActionMissingException(string message)
            : base(message)
        {
        }

        public ActionMissingException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

		protected ActionMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
