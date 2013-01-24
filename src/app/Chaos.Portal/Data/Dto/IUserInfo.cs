namespace Chaos.Portal.Data.Dto
{
    using System;

    using Chaos.Portal.Cache.Couchbase;
    using Chaos.Portal.Data.Dto.Standard;

    using CHAOS;
    using CHAOS.Serialization;

    public interface IUserInfo : IResult, ICacheable
    {
        [Serialize("GUID")]
        UUID GUID { get; set; }

        UUID SessionGUID { get; set; }

        [Serialize]
        long? SystemPermissions { get; set; }
        SystemPermissons SystemPermissonsEnum { get; set; }

        [Serialize]
        string Email { get; set; }

        [Serialize]
        DateTime? SessionDateCreated { get; set; }

        [Serialize]
        DateTime? SessionDateModified { get; set; } 
    }
}