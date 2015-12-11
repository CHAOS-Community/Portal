using System;
using CHAOS;
using CHAOS.Extensions;
using Chaos.Portal.Core.Data.Model;
using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace Chaos.Portal.v5.Dto
{
    public class UserSettings : AResult
    {
        #region Properties

        [Serialize("ClientSettingGUID")]
        public UUID ClientSettingGuid { get; set; }

        [Serialize("UserGUID")]
        public UUID UserGuid { get; set; }

        [SerializeXML(false, true)]
        [Serialize("Settings")]
        public string Settings { get; set; }

        [Serialize("DateCreated")]
        public DateTime DateCreated { get; set; }

        #endregion
        #region Constructor

        public UserSettings()
        {

        }

        public UserSettings(Core.Data.Model.UserSettings userSettings)
        {
            ClientSettingGuid = userSettings.ClientSettingGuid.ToUUID();
            UserGuid          = userSettings.UserGuid.ToUUID();
            Settings          = userSettings.Settings;
            DateCreated       = userSettings.DateCreated;
        }

        #endregion
    }
}