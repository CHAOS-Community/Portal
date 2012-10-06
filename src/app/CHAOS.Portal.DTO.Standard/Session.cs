using System;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
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

		public Session() : this( null, null, DateTime.MinValue, null)
		{
		}

		public Session( Guid guid, Guid userGUID, DateTime dateCreated, DateTime? dateModified ) : this( new UUID( guid.ToByteArray() ), new UUID( userGUID.ToByteArray() ), dateCreated, dateModified )
		{
		}

        public Session( UUID guid, UUID userGUID, DateTime dateCreated, DateTime? dateModified )
		{
			GUID         = guid;
			UserGUID     = userGUID;
			DateCreated  = dateCreated;
			DateModified = dateModified;
            Fullname     = "CHAOS.Portal.DTO.Standard.Session";
		}

		#endregion
	}
}
