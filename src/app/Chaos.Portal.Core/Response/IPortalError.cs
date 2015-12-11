namespace Chaos.Portal.Core.Response
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

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
        IPortalError InnerException { get; }

        void SetException(Exception exception);
    }
}