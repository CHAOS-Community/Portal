namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    public class UnsupportedExtensionReturnTypeException : System.Exception
    {
        public UnsupportedExtensionReturnTypeException()
        {
        }

        public UnsupportedExtensionReturnTypeException(string message)
            : base(message)
        {
        }

        public UnsupportedExtensionReturnTypeException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnsupportedExtensionReturnTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
