using System;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
	public class UserInfo : Result
	{
		#region Properties

        [Serialize("GUID")]
		public UUID GUID { get; set; }

		public UUID SessionGUID { get; set; }

		[Serialize]
		public long? SystemPermissions { get; set; }

        public SystemPermissons SystemPermissonsEnum { get; set; }

		[Serialize]
		public string Email { get; set; }

		[Serialize]
		public DateTime? SessionDateCreated { get; set; }

		[Serialize]
		public DateTime? SessionDateModified { get; set; }

		#endregion
		#region Construction

		public UserInfo() : this(Guid.Empty,null,null,null,null,null)
        {}

		public UserInfo( Guid uuid, Guid? sessionUUID, long? systemPermissions, string email, DateTime? dateCreated, DateTime? dateModified )
		{
			GUID                 = new UUID( uuid.ToByteArray() );
			SessionGUID          = sessionUUID.HasValue ? new UUID( sessionUUID.Value.ToByteArray() ) : null;
			SystemPermissions    = systemPermissions;
            SystemPermissonsEnum = systemPermissions.HasValue ? (SystemPermissons)systemPermissions : SystemPermissons.None;
			Email                = email;
			SessionDateCreated   = dateCreated;
			SessionDateModified  = dateModified;
            Fullname             = "CHAOS.Portal.DTO.Standard.UserInfo";
		}

		#endregion
	}
}

