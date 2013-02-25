namespace Chaos.Portal.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;

    using CHAOS.Data.MySql;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Exceptions;

    public class PortalRepository : IPortalRepository
    {
        #region Initialization

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

        #region Ticket

        public uint CreateTicket(Guid guid, uint ticketTypeID, string xml, string callback )
        {
//            using (var db = CreatePortalEntities())
//            {
//                return (uint) db.Ticket_Create(guid.ToByteArray(), (int?) ticketTypeID, xml, callback).First().Value;
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region User

        public uint CreateUser(Guid guid, string email)
        {
//            using(var db = this.CreatePortalEntities())
//            {
//                var result = db.User_Create(guid.ToByteArray(), email).FirstOrDefault();
//
//                if(!result.HasValue)
//                    throw new UnhandledException("Unknown exception in User Created on the database");
//
//                return (uint)result.Value;
            //            }

            throw new NotImplementedException();
        }

        public IUserInfo GetUserInfo(string email)
        {
//            var user = GetUserInfo(null, null, email).FirstOrDefault();
//            
//            if (user == null) throw new ArgumentException("Email not found"); // TODO: Replace with custom Exception
//
            //            return user;

            throw new NotImplementedException();
        }

        public IEnumerable<IUserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var users = db.UserInfo_Get(userGuid.HasValue ? userGuid.Value.ToByteArray() : null,
//                                            sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null, 
//                                            email);
//
//                return users.ToList().ToDto();
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region Group

        public IEnumerable<IGroup> GroupGet( Guid? guid, string name, Guid? requestedUserGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                return db.Group_Get(guid.HasValue ? guid.Value.ToByteArray() : null, 
//                                    name,
//                                    requestedUserGuid.HasValue ? requestedUserGuid.Value.ToByteArray() : null).ToList().ToDto();
            //            }

            throw new NotImplementedException();
        }

        public IGroup GroupCreate(Guid? guid, string name, Guid requestedUserGuid, uint systemPermission )
        {
//            guid = guid ?? new Guid();
//
//            using (var db = CreatePortalEntities())
//            {
//                var errorCode = new ObjectParameter("ErrorCode", 0);
//
//                db.Group_Create(guid.Value.ToByteArray(), 
//                                name, 
//                                requestedUserGuid.ToByteArray(), 
//                                (int?) systemPermission, 
//                                errorCode);
//
//                if (((int)errorCode.Value) == -100)
//                    throw new InsufficientPermissionsException("User has insufficient permissions to create groups");
//
//                if (((int)errorCode.Value) == -200)
//                    throw new UnhandledException("Group_Create was rolled back");
//            }

            //            return GroupGet(guid, name, requestedUserGuid).First();

            throw new NotImplementedException();
        }

        public uint GroupDelete(Guid guid, Guid userGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var errorCode = new ObjectParameter("ErrorCode", 0);
//
//                db.Group_Delete(guid.ToByteArray(), userGuid.ToByteArray(), errorCode);
//
//                if (((int)errorCode.Value) == -100)
//                    throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");
//
//                if (((int)errorCode.Value) == -200)
//                    throw new UnhandledException("Group_Delete was rolled back");
//
//                return 1;
            //            }

            throw new NotImplementedException();
        }

        public uint GroupUpdate(Guid guid, Guid userGuid, string newName, uint? newSystemPermission)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var result = db.Group_Update(newName, 
//                                             (int?) newSystemPermission, 
//                                             guid.ToByteArray(),
//                                             userGuid.ToByteArray()).FirstOrDefault();
//                if (!result.HasValue)
//                    throw new UnhandledException("An error occured on in Group_Delete and was rolled back");
//
//                if (result == -100)
//                    throw new InsufficientPermissionsException("User does not have permission to update group");
//
//                return(1);
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region Session

        public IEnumerable<ISession> SessionGet(Guid? guid, Guid? userGUID)
        {
//            using (var db = CreatePortalEntities())
//            {
//                return db.Session_Get(guid.HasValue ? guid.Value.ToByteArray() : null,
//                                      userGUID.HasValue ? userGUID.Value.ToByteArray() : null).ToList().ToDto();
            //            }

            throw new NotImplementedException();
        }

        public ISession SessionCreate(Guid userGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var sessionGUID = Guid.NewGuid();
//
//                db.Session_Create(sessionGUID.ToByteArray(), userGuid.ToByteArray());
//
//                return new Chaos.Portal.Data.Dto.Standard.Session(sessionGUID, userGuid, DateTime.Now, DateTime.Now);
            //            }

            throw new NotImplementedException();
        }

        public ISession SessionUpdate(Guid sessionGuid, Guid userGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var result = db.Session_Update(null, sessionGuid.ToByteArray(), userGuid.ToByteArray()).FirstOrDefault();
//
//                if(!result.HasValue)
//                    throw new UnhandledException("Session update failed on the database and was rolled back");
//
//                return db.Session_Get(sessionGuid.ToByteArray(), userGuid.ToByteArray()).ToDto().First();
            //            }

            throw new NotImplementedException();
        }

        public uint SessionDelete(Guid sessionGuid, Guid userGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var result = db.Session_Delete(sessionGuid.ToByteArray(), userGuid.ToByteArray()).FirstOrDefault();
//
//                if (!result.HasValue)
//                    throw new UnhandledException("Session delete failed on the database and was rolled back");
//
//                return (uint) result.Value;
            //            }

            throw new NotImplementedException();
        }

        #endregion
        #region Client Settings

        public IEnumerable<IClientSettings> ClientSettingsGet(Guid guid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                return db.ClientSettings_Get(guid.ToByteArray()).ToList().ToDto();
            //            }

            throw new NotImplementedException();
        }

        public uint ClientSettingsSet(Guid guid, string name, string settings)
        {
            throw new NotImplementedException("ClientSettingsSet not implemented");
        }

        #endregion
        #region Subscription

        public ISubscriptionInfo SubscriptionCreate(Guid? guid, string name, Guid requestingUserGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                guid = guid ?? new Guid();
//                var errorCode = new ObjectParameter("ErrorCode", 0);
//
//                db.Subscription_Create(guid.Value.ToByteArray(), name, requestingUserGuid.ToByteArray(), errorCode);
//
//                if (((int)errorCode.Value) == -100)
//                    throw new InsufficientPermissionsException("User does not have sufficient permissions to access the subscription");
//
//                var subscriptionInfo = db.SubscriptionInfo_Get(guid.Value.ToByteArray(), requestingUserGuid.ToByteArray()).ToDto().First();
//
//                return subscriptionInfo;
            //            }

            throw new NotImplementedException();
        }

        public IEnumerable<ISubscriptionInfo> SubscriptionGet(Guid? guid, Guid? requestingUserGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                return db.SubscriptionInfo_Get(guid.HasValue ? guid.Value.ToByteArray() : null,
//                                               requestingUserGuid.HasValue ? requestingUserGuid.Value.ToByteArray() : null).ToDto().ToList();
            //            }

            throw new NotImplementedException();
        }

        public uint SubscriptionDelete(Guid guid, Guid requestingUserGuid)
        {
//            using (var db = CreatePortalEntities())
//            {
//                var errorCode = new ObjectParameter("ErrorCode", 0);
//
//                db.Subscription_Delete(guid.ToByteArray(), requestingUserGuid.ToByteArray(), errorCode);
//
//                if (((int)errorCode.Value) == -100)
//                    throw new InsufficientPermissionsException("User does not have sufficient permissions to delete the subscription");
//
//                return 1;
            //            }

            throw new NotImplementedException();
        }

        public uint SubscriptionUpdate(Guid guid, string newName, Guid requestionUserGuid)
        {
//            var errorCode = new ObjectParameter("ErrorCode", 0);
//
//            using (var db = CreatePortalEntities())
//            {
//                db.Subscription_Update(guid.ToByteArray(), newName, requestionUserGuid.ToByteArray(), errorCode);
//            }
//
//            if (((int)errorCode.Value) == -100)
//                throw new InsufficientPermissionsException("User does not have sufficient permissions to access the subscription");
//
            //            return 1;

            throw new NotImplementedException();
        }

        #endregion
        #region User Settings

        public IEnumerable<IUserSettings> UserSettingsGet(Guid clientGuid, Guid userGuid)
        {
//            using(var db = CreatePortalEntities())
//            {
//                return db.UserSettings_Get(clientGuid.ToByteArray(), userGuid.ToByteArray()).ToDto();
            //            }

            throw new NotImplementedException();
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
//                var guid = sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null;
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