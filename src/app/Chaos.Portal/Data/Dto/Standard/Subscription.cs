namespace Chaos.Portal.Data.Dto.Standard
{
    using System;

    using CHAOS.Extensions;
    using CHAOS.Serialization;
    
    public class Subscription : Result
	{
		#region Properties

		[Serialize]
		public Guid     Guid { get; set; }

		[Serialize]
		public string   Name { get; set; }

		[Serialize]
		public DateTime DateCreated { get; set; }

		#endregion
		#region Contstruction

		public Subscription()
		{

		}

		public Subscription( Guid guid, string name, DateTime dateCreated )
		{
			Guid        = guid;
			Name        = name;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
