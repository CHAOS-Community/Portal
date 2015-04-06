using System;
using System.Runtime.Serialization;

namespace Chaos.Portal.Core.Exceptions
{
  public class ClientException : PortalException
  {
    public ClientException()
    {
    }

    public ClientException(string message, string userMessage) : base(message, userMessage)
    {
    }

    public ClientException(string message, string userMessage, Exception innerException) : base(message, userMessage, innerException)
    {
    }

    protected ClientException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}