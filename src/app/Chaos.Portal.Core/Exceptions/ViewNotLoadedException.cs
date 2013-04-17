namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

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
