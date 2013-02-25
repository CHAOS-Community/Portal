namespace Chaos.Portal.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS.Data;
    using CHAOS.Data.MySql;

    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Data.Mappings;
    using Chaos.Portal.Exceptions;

    using MySql.Data.MySqlClient;

    public class PortalRepository : IPortalRepository
    {
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

            return this;
        }

        #endregion
        #region Properties
        
        private Gateway Gateway { get; set; }

        #endregion

        #region Business Logic
        #region User

        public IEnumerable<UserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email)
        {
            return Gateway.ExecuteQuery<UserInfo>("UserInfo_Get", new[]
                {
                    new MySqlParameter("Guid", userGuid.HasValue ? userGuid.Value.ToByteArray() : null),
                    new MySqlParameter("SessionGuid", sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null),
                    new MySqlParameter("Email", email)
                });
        }

        public UserInfo GetUserInfo(string email)
        {
            var user = GetUserInfo(null, null, email).FirstOrDefault();
            
            if (user == null) throw new ArgumentException("Email not found"); // TODO: Replace with custom Exception

            return user;
        }

        #endregion
        #region Group

        public IEnumerable<Group> GroupGet( Guid? guid, string name, Guid? requestedUserGuid)
        {
            return Gateway.ExecuteQuery<Group>("Group_Get", new[]
                {
                    new MySqlParameter("Guid", guid.HasValue ? guid.Value.ToByteArray() : null), 
                    new MySqlParameter("Name", name), 
                    new MySqlParameter("RequestUserGuid", requestedUserGuid.HasValue ? requestedUserGuid.Value.ToByteArray() : null), 
                });
        }

        public Group GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission )
        {
            guid = guid ?? Guid.NewGuid();

            var result = Gateway.ExecuteNonQuery("Group_Create", new MySqlParameter[]
                {
                    new MySqlParameter("Guid", guid.Value.ToByteArray()),
                    new MySqlParameter("Name", name),
                    new MySqlParameter("RequestUserGuid", requestedUserGuid.ToByteArray()),
                    new MySqlParameter("SystemPermission", systemPermission),
                });

            if (result == -100) throw new InsufficientPermissionsException("User has insufficient permissions to create groups");
            if (result == -200) throw new UnhandledException("Group_Create had an unhandled exception and was rolled back");

            return GroupGet(guid, null, null).First();
        }

        public uint GroupDelete(Guid guid, Guid userGuid)
        {
            var result = Gateway.ExecuteNonQuery("Group_Delete", new[]
                {
                    new MySqlParameter("Guid", guid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray()), 
                });

            if(result == -100) throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");
            if(result == -200) throw new UnhandledException("Group_Delete had an unhandled exception and was rolled back");

            return (uint)result;
        }

        public uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission)
        {
            var result = Gateway.ExecuteNonQuery("Group_Update", new[]
                {
                    new MySqlParameter("NewName", newName), 
                    new MySqlParameter("NewSystemPermission", newSystemPermission), 
                    new MySqlParameter("WhereGroupGuid", guid.ToByteArray()), 
                    new MySqlParameter("RequestUserGuid", userGuid.ToByteArray()), 
                });

            if(result == -100) throw new InsufficientPermissionsException("User does not have permission to update group");
//          
            return (uint)result;
        }

        #endregion
        #region Session

        public IEnumerable<Session> SessionGet(Guid? guid, Guid? userGuid)
        {
            return Gateway.ExecuteQuery<Session>("Session_Get", new[]
                {
                    new MySqlParameter("Guid", guid.HasValue ? guid.Value.ToByteArray() : null), 
                    new MySqlParameter("UserGuid", userGuid.HasValue ? userGuid.Value.ToByteArray() : null), 
                });
        }

        public Session SessionCreate(Guid userGuid)
        {
            var guid = Guid.NewGuid();

            var result = Gateway.ExecuteNonQuery("Session_Create", new[]
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
                    new MySqlParameter("UserGuid", null), 
                    new MySqlParameter("WhereSessionGuid", guid.ToByteArray()), 
                    new MySqlParameter("WhereUserGuid", userGuid.ToByteArray()) 
                });

            return SessionGet(guid, userGuid).First();
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
                    new MySqlParameter("Settings", settings),
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
                    new MySqlParameter("RequestUserGuid", requestingUserGuid.ToByteArray()), 
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

        public IEnumerable<UserSettings> UserSettingsGet(Guid clientGuid, Guid userGuid)
        {
            return Gateway.ExecuteQuery<UserSettings>("UserSettings_Get", new[]
                {
                    new MySqlParameter("ClientSettingsGuid", clientGuid.ToByteArray()), 
                    new MySqlParameter("UserGuid", userGuid.ToByteArray()) 
                });
        }

        public uint UserSettingsSet(Guid clientGuid, Guid userGuid, string settings)
        {
//            using (var db = CreatePortalEntities())
//            {
//                db.UserSettings_Set(clientGuid.ToByteArray(), userGuid.ToByteArray(), settings);
//
//                return 1;
            //            }

            throw new NotImplementedException();
        }

        public uint UserSettingsDelete(Guid clientGuid, Guid userGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                db.UserSettings_Delete(clientGuid.ToByteArray(), userGuid.ToByteArray());
//
//                return 1;
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region Log

        public uint LogCreate(string name, Guid? sessionGuid, string loglevel, double? duration, string message)
        {
//            using(var db = CreatePortalEntities())
//            {
//                var guid = guid.HasValue ? guid.Value.ToByteArray() : null;
//
//                var result = db.Log_Create(name, loglevel, guid, duration, message).FirstOrDefault();
//
//                if (!result.HasValue)
//                    throw new UnhandledException("Unhandled exception occured in Log_Create, and was rolled back");
//
//                return (uint) result.Value;
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region Module

        public Module ModuleGet(string configurationName)
        {
//            using(var db = CreatePortalEntities())
//            {
//                return db.Module_Get(null, configurationName).ToDto().FirstOrDefault();
//            }

            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}