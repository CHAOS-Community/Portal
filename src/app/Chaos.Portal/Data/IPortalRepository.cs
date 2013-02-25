using System;
using System.Collections.Generic;
using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Data
{
    using Chaos.Portal.Data.Dto.Standard;

    public interface IPortalRepository
    {
        IPortalRepository WithConfiguration(string connectionString);

        IUserInfo GetUserInfo(string email);
        IEnumerable<UserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email);

        IEnumerable<ISubscriptionInfo> SubscriptionGet(Guid? guid, Guid? requestingUserGuid);
        ISubscriptionInfo SubscriptionCreate(Guid? guid, string name, Guid requestingUserGuid);
        uint SubscriptionUpdate(Guid guid, string newName, Guid requestionUserGuid);
        uint SubscriptionDelete(Guid guid, Guid requestingUserGuid);

        IEnumerable<Group> GroupGet(Guid? guid, string name, Guid? requestedUserGuid);
        IGroup GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission);
        uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission);
        uint GroupDelete(Guid guid, Guid userGuid);

        IEnumerable<ISession> SessionGet(Guid? guid, Guid? userGUID);
        ISession SessionCreate(Guid userGuid);
        ISession SessionUpdate(Guid sessionGuid, Guid userGuid);
        uint SessionDelete(Guid sessionGuid, Guid userGuid);

        IEnumerable<IClientSettings> ClientSettingsGet(Guid guid);
        uint ClientSettingsSet(Guid guid, string name, string settings);

        IEnumerable<IUserSettings> UserSettingsGet(Guid clientGuid, Guid userGuid);
        uint UserSettingsSet(Guid clientGuid, Guid userGuid, string settings);
        uint UserSettingsDelete(Guid clientGuid, Guid userGuid);

        uint LogCreate(string name, Guid? sessionGuid, string loglevel, double? duration, string message);

        Dto.Standard.Module ModuleGet(string configurationName);
    }
}
