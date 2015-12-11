namespace Chaos.Portal.Core.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Core.Data.Model;

    public class ModuleMapping : IReaderMapping<Module>
    {
        public IEnumerable<Module> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new Module
                    {
                        ID            = reader.GetUint32("ID"),
                        Name          = reader.GetString("Name"),
                        Configuration = reader.GetString("Configuration"),
                        DateCreated   = reader.GetDateTime("DateCreated")
                    };
            }
        }
    }
}