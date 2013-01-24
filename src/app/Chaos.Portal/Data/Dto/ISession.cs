namespace Chaos.Portal.Data.Dto
{
    using System;

    using Chaos.Portal.Cache.Couchbase;

    using CHAOS;
    using CHAOS.Serialization;

    /// <summary>
    /// The Session interface.
    /// </summary>
    public interface ISession : IResult, ICacheable
    {
        [Serialize("SessionGuid")]
        UUID GUID { get; set; }

        [Serialize("UserGUID")]
        UUID UserGUID { get; set; }

        [Serialize("DateCreated")]
        DateTime DateCreated { get; set; }

        [Serialize("DateModified")]
        DateTime? DateModified { get; set; } 
    }
}