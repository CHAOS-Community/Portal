using System;
using CHAOS;
using CHAOS.Portal.DTO;
using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace Chaos.Portal.Data.Dto
{
    public interface IUserSettings : IResult
    {
        [Serialize("ClientSettingGUID")]
        UUID ClientSettingGUID { get; set; }

        [Serialize("UserGUID")]
        UUID UserGUID { get; set; }

        [SerializeXML(false, true)]
        [Serialize("Settings")]
        string Settings { get; set; }

        [Serialize("DateCreated")]
        DateTime DateCreated { get; set; }
    }
}