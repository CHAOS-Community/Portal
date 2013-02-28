namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public class UserSettings : AResult
    {
        #region Properties

        [Serialize("ClientSettingGuid")]
		public Guid ClientSettingGuid { get; set; }

        [Serialize("UserGuid")]
		public Guid UserGuid { get; set; }

		[SerializeXML(false, true)]
		[Serialize("Settings")]
		public string Settings { get; set; }

        [Serialize("DateCreated")]
		public DateTime DateCreated { get; set; }

        #endregion
	}
}
