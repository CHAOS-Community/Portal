using System;
using System.Xml.Linq;
using Geckon;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class ClientSettings : Result
	{
		#region Properties

		[Serialize]
		public UUID GUID { get; set; }

		[Serialize]
		public string Name { get; set; }

		[Serialize]
		public XDocument Settings { get; set; }

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
			Settings    = XDocument.Parse( settings );
			DateCreated = dateCreated;
		}

		#endregion
	}
}
