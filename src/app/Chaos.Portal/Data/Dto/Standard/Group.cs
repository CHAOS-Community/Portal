using System;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto.Standard
{
	public class Group : Result, IGroup
	{
		#region Properties

		[Serialize]
		public UUID GUID { get; set; }

		[Serialize]
		public long? SystemPermission { get; set; }

		[Serialize]
		public string Name { get; set; }

		[Serialize]
		public DateTime DateCreated { get; set; }

		#endregion 
		#region Construction

		public Group()
		{
			
		}

		public Group( Guid guid, long? systemPermission, string name, DateTime dateCreated )
		{
			GUID             = new UUID( guid.ToByteArray() );
			SystemPermission = systemPermission;
			Name             = name;
			DateCreated      = dateCreated;
		}

		#endregion
	}
}
