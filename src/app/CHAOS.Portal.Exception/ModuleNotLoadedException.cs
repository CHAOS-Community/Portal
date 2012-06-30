using System.Runtime.Serialization;

namespace CHAOS.Portal.Exception
{
    public class ModuleNotLoadedException : System.Exception
    {
        public ModuleNotLoadedException()
        {
        }

        public ModuleNotLoadedException(string message)
            : base(message)
        {
        }

        public ModuleNotLoadedException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

		protected ModuleNotLoadedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
