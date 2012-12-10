using System;
using System.Collections.Generic;
using CHAOS.Portal.DTO.Standard;

namespace Chaos.Portal.Data
{
    public interface IPortalRepository
    {
        IPortalRepository WithConfiguration(string connectionString);

        uint CreateTicket(Guid guid, uint ticketTypeID, string xml, string callback);

        UserInfo GetUserInfo(string email);
        IEnumerable<UserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email);
    }
}
