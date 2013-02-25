using System;
using CHAOS;

namespace Chaos.Portal.Data.Dto.Standard
{
	public class User : Result
	{
		#region Properties

		public Guid Guid { get; set; }
		public string Email { get; set; }
		public DateTime DateCreated { get; set; }

		#endregion
	}
}

