using System;

using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto.Standard
{
    public class ClientSettings : Result, IClientSettings
	{
		#region Properties

		[Serialize]
		public Guid Guid { get; set; }

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
			Guid        = new Guid( guid );
			Name        = name;
			Settings    = settings;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
