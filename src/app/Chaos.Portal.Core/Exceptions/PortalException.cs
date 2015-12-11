using System;
using System.Runtime.Serialization;

namespace Chaos.Portal.Core.Exceptions
{
  public class PortalException : Exception
  {
    public string UserMessage { get; set; }

    public PortalException()
    {
    }

    public PortalException(string message, string userMessage) : base(message)
    {
      UserMessage = userMessage;
    }

    public PortalException(string message, string userMessage, Exception innerException) : base(message, innerException)
    {
      UserMessage = userMessage;
    }

    protected PortalException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}