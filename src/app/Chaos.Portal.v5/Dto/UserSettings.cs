namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    using Chaos.Portal.Core.Data.Model;

    public class UserSettings : AResult
    {
        #region Properties

        [Serialize("ClientSettingGUID")]
        public Guid ClientSettingGUID { get; set; }

        [Serialize("UserGUID")]
        public Guid UserGUID { get; set; }

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
            ClientSettingGUID = userSettings.ClientSettingGuid;
            UserGUID          = userSettings.UserGuid;
            Settings          = userSettings.Settings;
            DateCreated       = userSettings.DateCreated;
        }

        #endregion
    }
}