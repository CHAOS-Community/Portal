using System.Runtime.Serialization;

namespace Chaos.Portal.Exceptions
{
    public class ParameterBindingMissingException : System.Exception
    {
        public ParameterBindingMissingException()
        {
        }

        public ParameterBindingMissingException(string message)
            : base(message)
        {
        }

        public ParameterBindingMissingException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ParameterBindingMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
