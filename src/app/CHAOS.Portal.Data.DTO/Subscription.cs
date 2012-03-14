using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;

namespace CHAOS.Portal.Data.DTO
{
	public class Subscription : Result
	{
		#region Properties

		public UUID     GUID { get; set; }
		public string   Name { get; set; }
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
