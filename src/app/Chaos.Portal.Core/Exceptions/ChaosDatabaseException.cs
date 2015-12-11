using System;
using System.Runtime.Serialization;

namespace Chaos.Portal.Core.Exceptions
{
    public class ChaosDatabaseException : Exception
    {
        public ChaosDatabaseException()
        {
        }

        public ChaosDatabaseException(string message) : base(message)
        {
        }

        public ChaosDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ChaosDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}