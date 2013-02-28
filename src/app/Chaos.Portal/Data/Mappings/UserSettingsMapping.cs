namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto;

    public class UserSettingsMapping : IReaderMapping<UserSettings>
    {
        public IEnumerable<UserSettings> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new UserSettings
                    {
                        ClientSettingGuid = reader.GetGuid("ClientSettingsGUID"),
                        UserGuid          = reader.GetGuid("UserGUID"),
                        Settings          = reader.GetString("Settings"),
                        DateCreated       = reader.GetDateTime("DateCreated"),
                    };
            }
        }
    }
}