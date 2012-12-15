using System;
using CHAOS;
using CHAOS.Portal.DTO;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto
{
    public interface IUserInfo : IResult
    {
        [Serialize("GUID")]
        UUID GUID { get; set; }

        UUID SessionGUID { get; set; }

        [Serialize]
        long? SystemPermissions { get; set; }

        [Serialize]
        string Email { get; set; }

        [Serialize]
        DateTime? SessionDateCreated { get; set; }

        [Serialize]
        DateTime? SessionDateModified { get; set; } 
    }
}