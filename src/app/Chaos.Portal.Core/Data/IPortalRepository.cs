﻿namespace Chaos.Portal.Core.Data
{
  using System;
  using System.Collections.Generic;
  using Chaos.Portal.Core.Data.Model;

  public interface IPortalRepository
  {
    IPortalRepository WithConfiguration(string connectionString);

    UserInfo UserInfoGet(string email);
    IEnumerable<UserInfo> UserInfoGet(Guid? userGuid, Guid? sessionGuid, string email, Guid? groupGuid);
    IEnumerable<UserInfo> UserInfoGetWithGroupPermission(Guid userGuid);
    uint UserCreate(Guid guid, string email);

    /// <summary>
    /// Based on the Guid, either a User is created or updated.
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="email"></param>
    /// <returns>Returns 1 for success</returns>
    uint UserSet(Guid guid, string email);
    uint UserDelete(Guid guid);
    uint UserUpdate(Guid guid, string email, uint? SystemPermission);

    IEnumerable<SubscriptionInfo> SubscriptionGet(Guid? guid, Guid? requestingUserGuid);
    SubscriptionInfo SubscriptionCreate(Guid? guid, string name, Guid requestingUserGuid);
    uint SubscriptionUpdate(Guid guid, string newName, Guid requestionUserGuid);
    uint SubscriptionDelete(Guid guid, Guid requestingUserGuid);

    IEnumerable<Group> GroupGet(Guid? guid, string name, Guid? requestingUserGuid, Guid? userGuid);
    Group GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission);
    uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission);
    uint GroupDelete(Guid guid, Guid userGuid);
    uint GroupAddUser(Guid guid, Guid userGuid, uint systemPermission, Guid? requestUserGuid);
    uint GroupRemoveUser(Guid guid, Guid userGuid, Guid? requestUserGuid);
    uint GroupUpdateUserPermissions(Guid guid, Guid userGuid, uint permissions, Guid? requestingUserGuid);

    IEnumerable<Session> SessionGet(Guid? guid, Guid? userGuid);
    Session SessionCreate(Guid userGuid);
    Session SessionUpdate(Guid guid, Guid userGuid);
    uint SessionDelete(Guid guid, Guid userGuid);

    ClientSettings ClientSettingsGet(Guid guid);
    uint ClientSettingsSet(Guid guid, string name, string settings);

    UserSettings UserSettingsGet(Guid clientGuid, Guid userGuid);
    uint UserSettingsSet(Guid guid, Guid userGuid, string settings);
    uint UserSettingsDelete(Guid guid, Guid userGuid);

    uint LogCreate(string name, Guid? sessionGuid, string level, double? duration, string message);

    Module ModuleGet(string name);
    IModuleRepository Module { get; set; }
  }
}