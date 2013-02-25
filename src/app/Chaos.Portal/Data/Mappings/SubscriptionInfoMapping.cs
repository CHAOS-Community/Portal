namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto.Standard;

    public class SubscriptionInfoMapping : IReaderMapping<SubscriptionInfo>
    {
        public IEnumerable<SubscriptionInfo> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new SubscriptionInfo
                    {
                        Guid        = reader.GetGuid("GUID"),
                        Name        = reader.GetString("Name"),
                        UserGuid    = reader.GetGuid("UserGUID"),
                        Permission  = (SubscriptionPermission) reader.GetUint32("Permission"),
                        DateCreated = reader.GetDateTime("DateCreated")
                    };
            }
        }
    }
}