using System;
using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace Chaos.Portal.Response
{
    using Chaos.Portal.Response.Dto;

    public interface IPortalError
    {
        [SerializeXML(true)]
        [Serialize("Fullname")]
        string Fullname { get; }

        [Serialize("Message")]
        string Message { get; }

        [SerializeXML(false, true)]
        [Serialize("Stacktrace")]
        string Stacktrace { get; }

        [Serialize("InnerException")]
        PortalError InnerException { get; }

        void SetException(Exception exception);
    }
}