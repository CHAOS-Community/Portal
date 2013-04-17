namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

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
