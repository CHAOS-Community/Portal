using System;
using Geckon;

namespace CHAOS.Portal.DTO.Standard
{
	public class User : Result
	{
		#region Properties

		public UUID GUID { get; set; }
		public string Email { get; set; }
		public DateTime DateCreated { get; set; }

		#endregion
		#region Construction

		public User(){}

		public User( byte[] uuid, string email, DateTime dateCreated )
		{
			GUID        = new UUID( uuid );
			Email       = email;
			DateCreated = dateCreated;
		}

		#endregion
	}
}

