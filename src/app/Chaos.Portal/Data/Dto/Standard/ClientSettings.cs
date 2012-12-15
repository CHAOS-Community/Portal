using System;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto.Standard
{
    public class ClientSettings : Result, IClientSettings
	{
		#region Properties

		[Serialize]
		public UUID GUID { get; set; }

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

		public ClientSettings( byte[] guid, string name, string settings, DateTime dateCreated )
		{
			GUID        = new UUID( guid );
			Name        = name;
			Settings    = settings;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
