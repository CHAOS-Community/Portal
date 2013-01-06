using System.Runtime.Serialization;

namespace Chaos.Portal.Exceptions
{
    public class ViewNotLoadedException : System.Exception
    {
        public ViewNotLoadedException()
        {
        }

        public ViewNotLoadedException(string message)
            : base(message)
        {
        }

        public ViewNotLoadedException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ViewNotLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
