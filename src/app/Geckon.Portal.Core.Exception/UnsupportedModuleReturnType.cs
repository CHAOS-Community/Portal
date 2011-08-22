using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class UnsupportedModuleReturnType : System.Exception
    {
        public UnsupportedModuleReturnType()
        {
        }

        public UnsupportedModuleReturnType(string message) : base(message)
        {
        }

        public UnsupportedModuleReturnType(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedModuleReturnType(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
