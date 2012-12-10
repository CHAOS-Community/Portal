using System;
using System.Collections.Generic;
using System.Linq;
using Chaos.Portal.Data;

namespace CHAOS.Portal.Data.EF
{
    public class PortalRepository : IPortalRepository
    {
        #region Properties
        
        private string ConnectionString { get; set; }

        #endregion
        #region Construction

        public IPortalRepository WithConfiguration(string connectionString)
        {
            ConnectionString = connectionString;

            return this;
        }

        private PortalEntities CreatePortalEntities()
        {
            return ConnectionString == null ? new PortalEntities() : new PortalEntities(ConnectionString);
        }

        #endregion
        #region Business Logic

        #region Ticket

        public uint CreateTicket(Guid guid, uint ticketTypeID, string xml, string callback )
        {
            using (var db = CreatePortalEntities())
            {
                return (uint) db.Ticket_Create(guid.ToByteArray(), (int?) ticketTypeID, xml, callback);
            }
        }

        #endregion
        #region User

        public DTO.Standard.UserInfo GetUserInfo(string email)
        {
            var user = GetUserInfo(null, null, email).FirstOrDefault();
            
            if (user == null)
                throw new ArgumentException("Email not found"); // TODO: Replace with custom Exception

            return user;
        }

        public IEnumerable<DTO.Standard.UserInfo> GetUserInfo(Guid? userGuid, Guid? sessionGuid, string email)
        {
            using (var db = CreatePortalEntities())
            {
                var users = db.UserInfo_Get(userGuid.HasValue ? userGuid.Value.ToByteArray() : null,
                                            sessionGuid.HasValue ? sessionGuid.Value.ToByteArray() : null, 
                                            email);

                return users.ToDTO();
            }
        }

        #endregion

        #endregion
    }
}