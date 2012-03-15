using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class UnhandledException : System.Exception
    {
        public UnhandledException()
        {
        }

        public UnhandledException(string message) : base(message)
        {
        }

        public UnhandledException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

		protected UnhandledException(SerializationInfo info, StreamingContext context)
			: base(info, context)
        {
        }
    }
}
