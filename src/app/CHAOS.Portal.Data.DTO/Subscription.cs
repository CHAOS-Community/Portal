using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class Subscription : Result
	{
		#region Properties

		[Serialize]
		public UUID     GUID { get; set; }

		[Serialize]
		public string   Name { get; set; }

		[Serialize]
		public DateTime DateCreated { get; set; }

		#endregion
		#region Contstruction

		public Subscription()
		{

		}

		public Subscription( byte[] guid, string name, DateTime dateCreated )
		{
			GUID        = new UUID( guid );
			Name        = name;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
