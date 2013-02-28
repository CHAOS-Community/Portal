namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto;

    public class SubscriptionInfoMapping : IReaderMapping<SubscriptionInfo>
    {
        public IEnumerable<SubscriptionInfo> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new SubscriptionInfo
                    {
                        Guid        = reader.GetGuid("Guid"),
                        Name        = reader.GetString("Name"),
                        UserGuid    = reader.GetGuid("UserGuid"),
                        Permission  = (SubscriptionPermission) reader.GetUint32("Permission"),
                        DateCreated = reader.GetDateTime("DateCreated")
                    };
            }
        }
    }
}