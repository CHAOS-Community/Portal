namespace Chaos.Portal.Core.Data.Model
{
    using System;

    using CHAOS.Serialization;

    public class Group : AResult
	{
		#region Properties

		[Serialize]
		public Guid Guid { get; set; }

		[Serialize]
		public uint? SystemPermission { get; set; }

		[Serialize]
		public string Name { get; set; }

		[Serialize]
		public DateTime DateCreated { get; set; }

		#endregion 
	}
}
