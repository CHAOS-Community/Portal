namespace Chaos.Portal.Data.Dto
{
    using System;

    public class User : AResult
	{
		#region Properties

		public Guid Guid { get; set; }
		public string Email { get; set; }
		public DateTime DateCreated { get; set; }

		#endregion
	}
}

