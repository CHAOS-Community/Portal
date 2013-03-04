namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;

    public class UserInfo : AResult
	{
		#region Properties

        [Serialize("Guid")]
		public Guid Guid { get; set; }

		public Guid? SessionGuid { get; set; }

		[Serialize]
		public uint? SystemPermissions { get; set; }
        
        public SystemPermissons SystemPermissonsEnum
        {
            get
            {
                return (SystemPermissons)SystemPermissions;
            }
            set
            {
                SystemPermissions = (uint?)value;
            }
        }

		[Serialize]
		public string Email { get; set; }

		[Serialize]
		public DateTime? SessionDateCreated { get; set; }

		[Serialize]
		public DateTime? SessionDateModified { get; set; }

		#endregion
	}
}

