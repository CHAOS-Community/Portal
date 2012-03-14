using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;

namespace CHAOS.Portal.Data.DTO
{
	public class Group : Result
	{
		#region Properties

		public UUID GUID { get; set; }
		public long? SystemPermission { get; set; }
		public string Name { get; set; }
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
