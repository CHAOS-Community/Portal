using System;
using CHAOS;

namespace Chaos.Portal.Data.Dto.Standard
{
	public class User : Result
	{
		#region Properties

		public UUID GUID { get; set; }
		public string Email { get; set; }
		public DateTime DateCreated { get; set; }

		#endregion
		#region Construction

		public User() : this(null, null, DateTime.MinValue)
        {}

		public User( byte[] uuid, string email, DateTime dateCreated )
		{
			GUID        = new UUID( uuid );
			Email       = email;
			DateCreated = dateCreated;
            Fullname    = "CHAOS.Portal.DTO.Standard.User";
		}

		#endregion
	}
}

