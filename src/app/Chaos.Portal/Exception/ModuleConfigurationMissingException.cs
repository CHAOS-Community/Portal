using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
{
    public class ModuleConfigurationMissingException : System.Exception
    {
        public ModuleConfigurationMissingException()
        {
        }

        public ModuleConfigurationMissingException(string message)
            : base(message)
        {
        }

        public ModuleConfigurationMissingException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected ModuleConfigurationMissingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
