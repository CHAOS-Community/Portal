namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;

    using Core.Data.Model;

    public class Session : AResult
    {
        [Serialize("SessionGUID")]
        public Guid Guid { get; set; }
        [Serialize("UserGUID")]
        public Guid UserGuid { get; set; }
        [Serialize]
        public DateTime DateCreated { get; set; }
        [Serialize]
        public DateTime? DateModified { get; set; }

        public Session()
        {
            
        }

        public Session(Core.Data.Model.Session session)
        {
            Guid         = session.Guid;
            UserGuid     = session.UserGuid;
            DateCreated  = session.DateCreated;
            DateModified = session.DateModified;
        }
    }
}
