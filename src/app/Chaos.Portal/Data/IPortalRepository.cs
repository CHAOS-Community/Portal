using System;
using System.Collections.Generic;

namespace Chaos.Portal.Data
{
    using Chaos.Portal.Data.Dto.Standard;

    public interface IPortalRepository
    {
        IPortalRepository WithConfiguration(string connectionString);

        UserInfo GetUserInfo(string email);
        IEnumerable<UserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email);

        IEnumerable<SubscriptionInfo> SubscriptionGet(Guid? guid, Guid? requestingUserGuid);
        SubscriptionInfo SubscriptionCreate(Guid? guid, string name, Guid requestingUserGuid);
        uint SubscriptionUpdate(Guid guid, string newName, Guid requestionUserGuid);
        uint SubscriptionDelete(Guid guid, Guid requestingUserGuid);

        IEnumerable<Group> GroupGet(Guid? guid, string name, Guid? requestedUserGuid);
        Group GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission);
        uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission);
        uint GroupDelete(Guid guid, Guid userGuid);

        IEnumerable<Session> SessionGet(Guid? guid, Guid? userGuid);
        Session SessionCreate(Guid userGuid);
        Session SessionUpdate(Guid guid, Guid userGuid);
        uint SessionDelete(Guid guid, Guid userGuid);

        ClientSettings ClientSettingsGet(Guid guid);
        uint ClientSettingsSet(Guid guid, string name, string settings);

        IEnumerable<UserSettings> UserSettingsGet(Guid clientGuid, Guid userGuid);
        uint UserSettingsSet(Guid clientGuid, Guid userGuid, string settings);
        uint UserSettingsDelete(Guid clientGuid, Guid userGuid);

        uint LogCreate(string name, Guid? sessionGuid, string loglevel, double? duration, string message);

        Dto.Standard.Module ModuleGet(string configurationName);
    }
}
