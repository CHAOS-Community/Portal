namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto.Standard;

    public class ClientSettingsMapping : IReaderMapping<ClientSettings>
    {
        #region Implementation of IReaderMapping<out ClientSettings>

        public IEnumerable<ClientSettings> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new ClientSettings
                    {
                        Guid        = reader.GetGuid("Guid"),
                        Name        = reader.GetString("Name"),
                        Settings    = reader.GetString("Settings"),
                        DateCreated = reader.GetDateTime("DateCreated")
                    };
            }
        }

        #endregion
    }
}