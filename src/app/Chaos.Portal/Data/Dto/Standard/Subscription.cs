using System;
using CHAOS;
using CHAOS.Extensions;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto.Standard
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
