namespace Chaos.Portal.Core.Data.Model
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

