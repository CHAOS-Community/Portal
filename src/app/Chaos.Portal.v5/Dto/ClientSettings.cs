namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    public class ClientSettings : AResult
    {
        #region Properties

        [Serialize("GUID")]
        public Guid GUID { get; set; }

        [Serialize]
        public string Name { get; set; }

        [Serialize]
        public string Settings { get; set; }

        [Serialize]
        public DateTime DateCreated { get; set; }

        #endregion
        #region Constructors

        public ClientSettings()
        {

        }

        public ClientSettings(Core.Data.Model.ClientSettings clientSettings)
        {
            GUID        = clientSettings.Guid;
            Name        = clientSettings.Name;
            Settings    = clientSettings.Settings;
            DateCreated = clientSettings.DateCreated;
        }

        #endregion
    }
}