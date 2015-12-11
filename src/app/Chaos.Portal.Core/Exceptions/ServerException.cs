using System;
using System.Runtime.Serialization;

namespace Chaos.Portal.Core.Exceptions
{
  public class ServerException : PortalException
  {
    public ServerException()
    {
    }

    public ServerException(string message, string userMessage)
      : base(message, userMessage)
    {
    }

    public ServerException(string message, string userMessage, Exception innerException)
      : base(message, userMessage, innerException)
    {
    }

    protected ServerException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}