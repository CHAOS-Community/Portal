namespace Chaos.Portal.Core.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Core.Data.Model;

    public class GroupMapping : IReaderMapping<Group>
    {
        public IEnumerable<Group> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new Group
                    {
                        Guid             = reader.GetGuid("Guid"),
                        SystemPermission = reader.GetUint32("SystemPermission"),
                        Name             = reader.GetString("Name"),
                        DateCreated      = reader.GetDateTime("DateCreated"),
                    };
            }
        }

    }
}