namespace Chaos.Portal.Data.Dto.Standard
{
    using System;

    using CHAOS.Serialization;

    public class UserInfo : Result, IUserInfo
	{
		#region Properties

        [Serialize("Guid")]
		public Guid Guid { get; set; }

		public Guid? SessionGuid { get; set; }

		[Serialize]
		public uint? SystemPermissions { get; set; }

        public SystemPermissons SystemPermissonsEnum { get; set; }

		[Serialize]
		public string Email { get; set; }

		[Serialize]
		public DateTime? SessionDateCreated { get; set; }

		[Serialize]
		public DateTime? SessionDateModified { get; set; }

		#endregion
	}
}

