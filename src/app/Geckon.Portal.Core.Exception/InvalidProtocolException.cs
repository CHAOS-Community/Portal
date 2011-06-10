using System.Runtime.Serialization;

namespace Geckon.Portal.Core.Exception
{
    public class InvalidProtocolException : System.Exception
    {
        #region Contructors

        public InvalidProtocolException()
        {
        }

        public InvalidProtocolException(string message): base(message)
        {
        }

        public InvalidProtocolException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion
    }
}
