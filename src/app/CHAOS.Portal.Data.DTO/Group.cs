using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class Group : Result
	{
		#region Properties

		[Serialize]
		public UUID GUID { get; set; }

		[Serialize]
		public long? SystemPermission { get; set; }

		[Serialize]
		public string Name { get; set; }

		[Serialize]
		public DateTime DateCreated { get; set; }

		#endregion 
		#region Construction

		public Group()
		{
			
		}

		public Group( byte[] guid, long? systemPermission, string name, DateTime dateCreated )
		{
			GUID             = new UUID( guid );
			SystemPermission = systemPermission;
			Name             = name;
			DateCreated      = dateCreated;
		}

		#endregion
	}
}
