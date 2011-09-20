using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class CallContext : ICallContext
    {
        #region Properties

        public ICache Cache { get; set; }
        public ISolr  Solr { get; set; }
        public string SessionID { get; set; }
        public IEnumerable<Parameter> Parameters { get;  set; }

        public UserInfo User
        {
            get
            {
                UserInfo userInfo = Cache.Get<UserInfo>( string.Format( "[UserInfo:sid={0}]", SessionID ) );

                if (userInfo == null)
                {
                    using( PortalDataContext db = PortalDataContext.Default() )
                    {
                        userInfo = db.UserInfo_Get( null, Guid.Parse( SessionID ), null, null, null ).First();

                        Cache.Put( string.Format("[UserInfo:sid={0}]", SessionID),
                                   userInfo,
                                   new TimeSpan(0, 1, 0) );
                    }
                }

                return userInfo;
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

        public CallContext( ICache cache, ISolr solr, string sessionID ) : this()
        {
            Cache     = cache;
            Solr      = solr;
            SessionID = sessionID;
        }

        public CallContext()
        {
        }

        #endregion
    }
}
