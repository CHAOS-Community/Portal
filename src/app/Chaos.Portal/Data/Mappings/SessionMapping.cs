namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto;

    public class SessionMapping : IReaderMapping<Session> 
    {
        public IEnumerable<Session> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new Session
                    {
                        Guid         = reader.GetGuid("Guid"),
                        UserGuid     = reader.GetGuid("UserGuid"),
                        DateCreated  = reader.GetDateTime("DateCreated"),
                        DateModified = reader.GetDateTimeNullable("DateModified")
                    };
            }
        }
    }
}