using System;
using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace CHAOS.Portal.DTO.Standard
{
	public class UserSettings : Result
    {
        #region Properties

        [Serialize("ClientSettingGUID")]
		public UUID ClientSettingGUID { get; set; }

        [Serialize("UserGUID")]
		public UUID UserGUID { get; set; }

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

		public UserSettings( byte[] clientSettingGUID, byte[] userGUID, string settings, DateTime dateCreated )
		{
			ClientSettingGUID = new UUID( clientSettingGUID );
			UserGUID          = new UUID( userGUID );
			Settings          = settings;
			DateCreated       = dateCreated;
		}

		#endregion
	}
}
