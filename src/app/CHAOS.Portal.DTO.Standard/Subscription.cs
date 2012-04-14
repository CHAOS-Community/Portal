using System;
using Geckon;
using Geckon.Serialization;

namespace CHAOS.Portal.DTO.Standard
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

		public Subscription( Guid guid, string name, DateTime dateCreated )
		{
			GUID        = guid.ToUUID();
			Name        = name;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
