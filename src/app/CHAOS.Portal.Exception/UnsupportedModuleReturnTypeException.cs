using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
{
    public class UnsupportedModuleReturnTypeException : System.Exception
    {
        public UnsupportedModuleReturnTypeException()
        {
        }

        public UnsupportedModuleReturnTypeException(string message)
            : base(message)
        {
        }

        public UnsupportedModuleReturnTypeException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnsupportedModuleReturnTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
