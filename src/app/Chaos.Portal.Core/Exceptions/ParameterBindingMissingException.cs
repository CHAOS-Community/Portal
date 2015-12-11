namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

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
