namespace Chaos.Portal.Core.Exceptions
{
    using System.Runtime.Serialization;

    public class InvalidViewDataException : System.Exception
    {
        public InvalidViewDataException()
        {
        }

        public InvalidViewDataException(string message)
            : base(message)
        {
        }

        public InvalidViewDataException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected InvalidViewDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
