using System;
using CHAOS;
using CHAOS.Serialization;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Data.Dto
{
    public interface IUserInfo : IResult
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