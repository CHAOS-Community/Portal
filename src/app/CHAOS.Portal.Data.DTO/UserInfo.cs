using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;

namespace CHAOS.Portal.Data.DTO
{
	public class UserInfo : Result
	{
		#region Properties

		public UUID GUID { get; set; }
		public UUID SessionGUID { get; set; }
		public long? SystemPermissions { get; set; }
		public string Email { get; set; }
		public DateTime? SessionDateCreated { get; set; }
		public DateTime? SessionDateModified { get; set; }

		#endregion
		#region Construction

		public UserInfo(){}

		public UserInfo( byte[] uuid, byte[] sessionUUID, long? systemPermissions, string email, DateTime? dateCreated, DateTime? dateModified )
		{
			GUID                = new UUID( uuid );
			SessionGUID         = new UUID( sessionUUID );
			SystemPermissions   = systemPermissions;
			Email               = email;
			SessionDateCreated  = dateCreated;
			SessionDateModified = dateModified;
		}

		#endregion
	}
}

