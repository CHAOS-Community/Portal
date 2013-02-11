namespace Chaos.Portal.Data.Dto.Standard
{
    using System;

    using CHAOS.Serialization;

    public class UserInfo : Result, IUserInfo
	{
		#region Properties

        public string DocumentID
        {
            get
            {
                return this.Guid.ToString();
            }
            set
            {
                this.Guid = new Guid(value);
            }
        }

        [Serialize("Guid")]
		public Guid Guid { get; set; }

		public Guid? SessionGuid { get; set; }

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

		public UserInfo( Guid guid, Guid? sessionGuid, long? systemPermissions, string email, DateTime? dateCreated, DateTime? dateModified )
		{
			Guid                 = guid;
			SessionGuid          = sessionGuid;
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

