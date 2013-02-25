namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public interface IUserSettings : IResult
    {
        [Serialize("ClientSettingGuid")]
        Guid ClientSettingGuid { get; set; }

        [Serialize("UserGuid")]
        Guid UserGuid { get; set; }

        [SerializeXML(false, true)]
        [Serialize("Settings")]
        string Settings { get; set; }

        [Serialize("DateCreated")]
        DateTime DateCreated { get; set; }
    }
}