using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class UserInfo : Result
	{
		#region Properties

		[Serialize]
		public UUID GUID { get; set; }

		[Serialize]
		public UUID SessionGUID { get; set; }

		[Serialize]
		public long? SystemPermissions { get; set; }

		[Serialize]
		public string Email { get; set; }

		[Serialize]
		public DateTime? SessionDateCreated { get; set; }

		[Serialize]
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

