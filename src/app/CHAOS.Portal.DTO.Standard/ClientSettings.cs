using System;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
	public class ClientSettings : Result
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
