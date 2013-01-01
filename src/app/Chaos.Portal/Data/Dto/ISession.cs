using System;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto
{
    public interface ISession : IResult
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