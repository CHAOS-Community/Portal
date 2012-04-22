using System;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
	public class Group : Result
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
