namespace Chaos.Portal.Core.Data.Model
{
    using System;

    using CHAOS.Serialization;

    public class ClientSettings : AResult
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
