using System;
using Geckon;
using Geckon.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
	public class Session : Result
	{
		#region Properties

		[Serialize("SessionGUID")]
		public string GUID { get; set; }

		[Serialize("UserGUID")]
        public string UserGUID { get; set; }

		[Serialize("DateCreated")]
		public DateTime DateCreated { get; set; }

		[Serialize("DateModified")]
		public DateTime? DateModified { get; set; }

		#endregion
		#region Construction

		public Session()
		{
		}

		public Session( Guid guid, Guid userGUID, DateTime dateCreated, DateTime? dateModified ) : this( new UUID( guid.ToByteArray() ), new UUID( userGUID.ToByteArray() ), dateCreated, dateModified )
		{
		}

        public Session( UUID guid, UUID userGUID, DateTime dateCreated, DateTime? dateModified )
		{
			GUID         = guid.ToString();
			UserGUID     = userGUID.ToString();
			DateCreated  = dateCreated;
			DateModified = dateModified;
		}

		#endregion
	}
}
