using System;
using Geckon;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class Session : Result
	{
		#region Properties

		[Serialize("SessionGUID")]
		public UUID GUID { get; set; }

		[Serialize("UserGUID")]
		public UUID UserGUID { get; set; }

		[Serialize("DateCreated")]
		public DateTime DateCreated { get; set; }

		[Serialize("DateModified")]
		public DateTime? DateModified { get; set; }

		#endregion
		#region Construction

		public Session()
		{
			
		}

		public Session( byte[] guid, byte[] userGUID, DateTime dateCreated, DateTime? dateModified )
		{
			GUID         = new UUID( guid );
			UserGUID     = userGUID == null ? UUID.Empty : new UUID( userGUID );
			DateCreated  = dateCreated;
			DateModified = dateModified;
		}

		#endregion
	}
}
