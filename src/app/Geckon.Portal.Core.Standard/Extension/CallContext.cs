using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Data;
using System.Configuration;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class CallContext : ICallContext
    {
        #region Properties

        public ICache Cache { get; private set; }
        public IIndex Solr { get; private set; }
        public Guid? SessionID { get; set; }

        public UserInfo User
        {
            get
            {
                UserInfo userInfo = Cache.Get<UserInfo>( string.Format( "[UserInfo:sid={0}]", SessionID ) );

                if (userInfo == null)
                {
                    using( PortalDataContext db = PortalDataContext.Default() )
                    {
                        userInfo = db.UserInfo_Get( null, SessionID, null ).First();

                        Cache.Put( string.Format("[UserInfo:sid={0}]", SessionID),
                                   userInfo,
                                   new TimeSpan(0, 1, 0) );
                    }
                }

                return userInfo;
            }
        }

        public Guid AnonymousUserGUID
        {
            get
            {
                string guid = (string)Cache.Get("AnonymousUserGUID");

                if (string.IsNullOrEmpty(guid))
                    guid = ConfigurationManager.AppSettings["AnonymousUserGUID"];

                return new Guid(guid);
            }
        }

        public IEnumerable<Group> Groups
        {
            get
            {
                using( PortalDataContext db = PortalDataContext.Default() )
                {
                    return db.Group_Get( null, null, null, User.ID ).ToList();
                }
            }
        }

        public IEnumerable<SubscriptionInfo> Subscriptions
        {
            get
            {
                using (PortalDataContext db = PortalDataContext.Default())
                {
                    return db.SubscriptionInfo_Get( null, null, null, User.ID ).ToList();
                }
            }
        }

        #endregion
        #region Construction

        public CallContext( ICache cache, IIndex solr, string sessionID )
        {
            Cache      = cache;
            Solr       = solr;
            SessionID  = String.IsNullOrEmpty( sessionID ) ? (Guid?) null : Guid.Parse( sessionID );
        }

        #endregion
    }
}
