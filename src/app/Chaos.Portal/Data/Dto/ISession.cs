namespace Chaos.Portal.Data.Dto
{
    using System;

    using Chaos.Portal.Cache.Couchbase;

    using CHAOS.Serialization;

    /// <summary>
    /// The Session interface.
    /// </summary>
    public interface ISession : IResult, ICacheable
    {
        [Serialize("SessionGuid")]
        Guid Guid { get; set; }

        [Serialize("UserGuid")]
        Guid UserGuid { get; set; }

        [Serialize("DateCreated")]
        DateTime DateCreated { get; set; }

        [Serialize("DateModified")]
        DateTime? DateModified { get; set; } 
    }
}