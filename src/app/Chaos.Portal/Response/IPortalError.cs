using System;
using CHAOS.Serialization;
using CHAOS.Serialization.XML;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Response
{
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