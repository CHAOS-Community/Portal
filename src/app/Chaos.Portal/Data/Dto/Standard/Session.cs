namespace Chaos.Portal.Data.Dto.Standard
{
    using System;

    using CHAOS.Serialization;

	public class Session : Result, ISession
	{
		#region Properties

        public string DocumentID 
        { 
            get
            {
                return Guid.ToString();
            }
            set
            {
                Guid = new Guid(value);
            }
        }

        [Serialize]
		public Guid Guid { get; set; }
        [Serialize]
        public Guid UserGuid { get; set; }
        [Serialize]
        public DateTime DateCreated { get; set; }
        [Serialize]
        public DateTime? DateModified { get; set; }

		#endregion
		#region Construction

		public Session() : this( Guid.Empty, Guid.Empty, DateTime.MinValue, null)
		{
		}

        public Session( Guid guid, Guid userGuid, DateTime dateCreated, DateTime? dateModified )
		{
			Guid         = guid;
			UserGuid     = userGuid;
			DateCreated  = dateCreated;
			DateModified = dateModified;
            Fullname     = "CHAOS.Portal.DTO.Standard.Session";
		}

		#endregion
	}
}
