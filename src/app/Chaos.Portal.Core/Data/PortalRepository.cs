namespace Chaos.Portal.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using CHAOS.Data;
    using CHAOS.Data.MySql;

    using Mappings;
    using Model;
    using Exceptions;

    using MySql.Data.MySqlClient;

    public class PortalRepository : IPortalRepository
    {
        public IModuleRepository Module { get; set; }

        #region Initialization

        static PortalRepository()
        {
            // todo: add better error message when mapping is missing
            ReaderExtensions.Mappings.Add(typeof(SubscriptionInfo), new SubscriptionInfoMapping());
            ReaderExtensions.Mappings.Add(typeof(ClientSettings), new ClientSettingsMapping());
            ReaderExtensions.Mappings.Add(typeof(UserSettings), new UserSettingsMapping());
            ReaderExtensions.Mappings.Add(typeof(UserInfo), new UserInfoMapping());
            ReaderExtensions.Mappings.Add(typeof(Session), new SessionMapping());
            ReaderExtensions.Mappings.Add(typeof(Group), new GroupMapping());
        }

        public IPortalRepository WithConfiguration(string connectionString)
        {
            Gateway = new Gateway(connectionString);
            
            Module = new ModuleRepository(Gateway);
            
            return this;
        }

        #endregion
        #region Properties
        
        private Gateway Gateway { get; set; }

        #endregion

        #region Business Logic
        #region User

		public IEnumerable<UserInfo> UserInfoGet(Guid? userGuid, Guid? sessionGuid, string email, Guid? groupGuid)
        {
            return Gateway.ExecuteQuery<UserInfo>("UserInfo_Get", new[]
                {
                    new MySqlParameter("Guid", userGuid.HasValue ? userGuid.Value.ToByteArray() : null),
                    new MySqlParameter("SessionGuid", sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null),
                    new MySqlParameter("Email", email),
					new MySqlParameter("GroupGuid", groupGuid.HasValue ? groupGuid.Value.ToByteArray() : null)
                });
        }

	    public IEnumerable<UserInfo> UserInfoGetWithGroupPermission(Guid userGuid)
	    {
			return Gateway.ExecuteQuery<UserInfo>("UserInfo_GetWithGroupPermission", new[]
                {
                    new MySqlParameter("Guid", userGuid.ToByteArray())
                });
	    }

	    public uint UserCreate(Guid guid, string email)
        {
            var result = Gateway.ExecuteNonQuery("User_Create", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("Email", email)
                });

            return (uint)result;
        }

      public uint UserSet(Guid guid, string email)
      {
        var result = Gateway.ExecuteNonQuery("User_Set", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("Email", email)
                });

        return (uint)result;
      }

	    public uint UserDelete(Guid guid)
	    {
        var result = Gateway.ExecuteNonQuery("User_Delete", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray())
                });

        return (uint)result;
	    }

	    public uint UserUpdate(Guid guid, string email, uint? systemPermission)
	    {
	      return UserSet(guid, email);
	    }

	    public UserInfo UserInfoGet(string email)
        {
            var user = UserInfoGet(null, null, email, null).FirstOrDefault();
            
            if (user == null) throw new ArgumentException("Email not found"); // TODO: Replace with custom Exception

            return user;
        }

        #endregion
        #region Group

		public IEnumerable<Group> GroupGet(Guid? guid, string name, Guid? requestingUserGuid, Guid? userGuid)
        {
            return Gateway.ExecuteQuery<Group>("Group_Get", new[]
                {
                    new MySqlParameter("Guid", guid.HasValue ? guid.Value.ToByteArray() : null), 
                    new MySqlParameter("Name", name), 
                    new MySqlParameter("RequestingUserGuid", requestingUserGuid.HasValue ? requestingUserGuid.Value.ToByteArray() : null), 
                    new MySqlParameter("UserGuid", userGuid.HasValue ? userGuid.Value.ToByteArray() : null) 
                });
        }

	    public Group GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission )
        {
            guid = guid ?? Guid.NewGuid();

            var result = Gateway.ExecuteNonQuery("Group_Create", new[]
                {
                    new MySqlParameter("Guid", guid.Value.ToByteArray()),
                    new MySqlParameter("Name", name),
                    new MySqlParameter("RequestUserGuid", requestedUserGuid.ToByteArray()),
                    new MySqlParameter("SystemPermission", systemPermission)
                });

            if (result == -100) throw new InsufficientPermissionsException("User has insufficient permissions to create groups");
            if (result == -200) throw new UnhandledException("Group_Create had an unhandled exception and was rolled back");

            return GroupGet(guid, null, null, null).First();
        }

		public uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission)
		{
			var result = Gateway.ExecuteNonQuery("Group_Update", new[]
                {
                    new MySqlParameter("NewName", newName), 
                    new MySqlParameter("NewSystemPermission", newSystemPermission), 
                    new MySqlParameter("WhereGroupGuid", guid.ToByteArray()), 
                    new MySqlParameter("RequestUserGuid", userGuid.ToByteArray()) 
                });

			if (result == -100) throw new InsufficientPermissionsException("User does not have permission to update group");

			return (uint)result;
		}

        public uint GroupDelete(Guid guid, Guid userGuid)
        {
            var result = Gateway.ExecuteNonQuery("Group_Delete", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray()) 
                });

            if(result == -100) throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");
            if(result == -200) throw new UnhandledException("Group_Delete had an unhandled exception and was rolled back");

            return (uint)result;
        }

	    public uint GroupAddUser(Guid guid, Guid userGuid, uint systemPermission, Guid? requestUserGuid)
	    {
			var result = Gateway.ExecuteNonQuery("Group_AssociateWithUser", new[]
                {
                    new MySqlParameter("GroupGUID", guid.ToByteArray()), 
                    new MySqlParameter("UserGUID", userGuid.ToByteArray()), 
                    new MySqlParameter("Permission", systemPermission), 
                    new MySqlParameter("RequestUserGUID", requestUserGuid.HasValue ? requestUserGuid.Value.ToByteArray() : null) 
                });

			if (result == -100) throw new InsufficientPermissionsException("User does not have permission to add user to group");

			return (uint)result;
	    }

	    public uint GroupRemoveUser(Guid guid, Guid userGuid, Guid? requestUserGuid)
	    {
			var result = Gateway.ExecuteNonQuery("Group_RemoveUser", new[]
                {
                    new MySqlParameter("GroupGUID", guid.ToByteArray()), 
                    new MySqlParameter("UserGUID", userGuid.ToByteArray()), 
                    new MySqlParameter("RequestUserGUID", requestUserGuid.HasValue ? requestUserGuid.Value.ToByteArray() : null)
                });

			if (result == -100) throw new InsufficientPermissionsException("User does not have permission to remove user from group");

			return (uint)result;
	    }

		public uint GroupUpdateUserPermissions(Guid guid, Guid userGuid, uint permissions, Guid? requestingUserGuid)
	    {
			var result = Gateway.ExecuteNonQuery("Group_UpdateUserPermissions", new[]
                {
                    new MySqlParameter("GroupGUID", guid.ToByteArray()), 
                    new MySqlParameter("UserGUID", userGuid.ToByteArray()), 
                    new MySqlParameter("Permissions", permissions), 
					new MySqlParameter("RequestingUserGUID", requestingUserGuid.HasValue ? requestingUserGuid.Value.ToByteArray() : null)
                });

			if (result == -100) throw new InsufficientPermissionsException("User does not have permission to remove user from group");

			return (uint)result;
	    }

	    #endregion
        #region Session

        public IEnumerable<Session> SessionGet(Guid? guid, Guid? userGuid)
        {
            return Gateway.ExecuteQuery<Session>("Session_Get", new[]
                {
                    new MySqlParameter("Guid", guid.HasValue ? guid.Value.ToByteArray() : null), 
                    new MySqlParameter("UserGuid", userGuid.HasValue ? userGuid.Value.ToByteArray() : null) 
                });
        }

        public Session SessionCreate(Guid userGuid)
        {
            var guid = Guid.NewGuid();

            Gateway.ExecuteNonQuery("Session_Create", new[]
            {
                new MySqlParameter("Guid", guid.ToByteArray()), 
                new MySqlParameter("UserGuid", userGuid.ToByteArray())
            });

            return SessionGet(guid, null).First();
        }

        public Session SessionUpdate(Guid guid, Guid userGuid)
        {
            var result = Gateway.ExecuteNonQuery("Session_Update", new[]
            {
                new MySqlParameter("UserGuid", userGuid.ToByteArray()), 
                new MySqlParameter("WhereSessionGuid", guid.ToByteArray()), 
                new MySqlParameter("WhereUserGuid", null) 
            });

						if(result == 0) throw new SessionDoesNotExistException("Session was not updated because it doesn't exist");

            return SessionGet(guid, userGuid).Single();
        }

        public uint SessionDelete(Guid guid, Guid userGuid)
        {
            var result = Gateway.ExecuteNonQuery("Session_Delete", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray())
                });

            return (uint)result;
        }

        #endregion
        #region Client Settings

        public ClientSettings ClientSettingsGet(Guid guid)
        {
            return Gateway.ExecuteQuery<ClientSettings>("ClientSettings_Get", new MySqlParameter("Guid", guid.ToByteArray()) ).FirstOrDefault();
        }

        public uint ClientSettingsSet(Guid guid, string name, string settings)
        {
            var result = Gateway.ExecuteNonQuery("ClientSettings_Set", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("Name", name), 
                    new MySqlParameter("Settings", settings)
                });

            return (uint)result;
        }

        #endregion
        #region Subscription

        public IEnumerable<SubscriptionInfo> SubscriptionGet(Guid? guid, Guid? requestingUserGuid)
        {
            return Gateway.ExecuteQuery<SubscriptionInfo>("SubscriptionInfo_Get", new[]
                {
                    new MySqlParameter("Guid", guid.HasValue ? guid.Value.ToByteArray() : null), 
                    new MySqlParameter("RequestUserGuid", requestingUserGuid.HasValue ? requestingUserGuid.Value.ToByteArray() : null)
                });
        }

        public SubscriptionInfo SubscriptionCreate(Guid? guid, string name, Guid requestingUserGuid)
        {
            guid = guid ?? Guid.NewGuid();

            var result = Gateway.ExecuteNonQuery("Subscription_Create", new[]
                {
                    new MySqlParameter("Guid", guid.Value.ToByteArray()), 
                    new MySqlParameter("Name", name), 
                    new MySqlParameter("RequestUserGuid", requestingUserGuid.ToByteArray()) 
                });

            if(result == -100) throw new InsufficientPermissionsException("User does not have sufficient permissions to access the subscription");
            if(result == -200) throw new UnhandledException("Unhanded exception in Subscription_Create and was rolled back");

            return SubscriptionGet(guid, requestingUserGuid).First();
        }

        public uint SubscriptionDelete(Guid guid, Guid requestingUserGuid)
        {
            var result = Gateway.ExecuteNonQuery("Subscription_Delete", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("RequestingUserGuid", requestingUserGuid.ToByteArray()) 
                });

            if(result == -100) throw new InsufficientPermissionsException("User does not have sufficient permissions to delete the subscription");
            if(result == -200) throw new UnhandledException("Unhanded exception in Subscription_Delete and was rolled back");
            
            return (uint)result;
        }

        public uint SubscriptionUpdate(Guid guid, string newName, Guid requestingUserGuid)
        {
            var result = Gateway.ExecuteNonQuery("Subscription_Update", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("NewName", newName), 
                    new MySqlParameter("RequestingUserGuid", requestingUserGuid.ToByteArray()) 
                });

            if (result == -100) throw new InsufficientPermissionsException("User does not have sufficient permissions to update the subscription");

            return (uint)result;
        }

        #endregion
        #region User Settings

        public UserSettings UserSettingsGet(Guid clientGuid, Guid userGuid)
        {
            var result = Gateway.ExecuteQuery<UserSettings>("UserSettings_Get", new[]
            {
                new MySqlParameter("ClientSettingsGuid", clientGuid.ToByteArray()), 
                new MySqlParameter("UserGuid", userGuid.ToByteArray()) 
            }).FirstOrDefault();

            if (result == null)
                throw new ChaosDatabaseException("User has no settings on the client");

            return result;
        }

        public uint UserSettingsSet(Guid guid, Guid userGuid, string settings)
        {
            var result = Gateway.ExecuteNonQuery("UserSettings_Set", new[]
                {
                    new MySqlParameter("ClientSettingsGuid", guid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray()), 
                    new MySqlParameter("Settings", settings)
                });

            return (uint)result;
        }

        public uint UserSettingsDelete(Guid guid, Guid userGuid)
        {
            var result = Gateway.ExecuteNonQuery("UserSettings_Delete", new[]
                {
                    new MySqlParameter("ClientSettingsGuid", guid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray())
                });

            return (uint)result;
        }

        #endregion
        #region Log

        public uint LogCreate(string name, Guid? sessionGuid, string level, double? duration, string message)
        {
            var result = Gateway.ExecuteNonQuery("Log_Create", new[]
                {
                    new MySqlParameter("Name",name), 
                    new MySqlParameter("Level",level), 
                    new MySqlParameter("SessionGuid",sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null), 
                    new MySqlParameter("Duration",duration), 
                    new MySqlParameter("Message",message) 
                });

            return (uint)result;
        }

        #endregion
        #region Module

        public Module ModuleGet(string name)
        {
            return Module.Get(name);
        }

        #endregion

        #endregion
    }
}