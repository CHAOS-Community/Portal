using System;
using CHAOS;

namespace Chaos.Portal.Data.Dto.Standard
{
	public class Session : Result, ISession
	{
		#region Properties

		public UUID GUID { get; set; }
        public UUID UserGUID { get; set; }
		public DateTime DateCreated { get; set; }
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
