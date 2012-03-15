using System;
using System.Collections.Generic;
using System.Linq;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Extension;
using System.Configuration;
using Geckon.Portal.Core.Cache;
using Geckon.Index;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class CallContext : ICallContext
    {
        #region Properties

        public ICache Cache { get; private set; }
        public IIndexManager IndexManager { get; private set; }
        public UUID SessionGUID { get; set; }

		public CHAOS.Portal.Data.DTO.UserInfo User
        {
            get
            {
				CHAOS.Portal.Data.DTO.UserInfo userInfo = Cache.Get<CHAOS.Portal.Data.DTO.UserInfo>(string.Format("[UserInfo:sid={0}]", SessionGUID));

                if( userInfo == null )
                {
					using( PortalEntities db = new PortalEntities() )
                    {
                        userInfo = db.UserInfo_Get( null, SessionGUID.ToByteArray() ).ToDTO().FirstOrDefault();

                        if( userInfo == null )
                            throw new SessionDoesNotExist( "Session has expired" );

						Cache.Put( string.Format( "[UserInfo:sid={0}]", SessionGUID ),
								   userInfo,
								   new TimeSpan( 0, 1, 0 ) );
                    }
                }

                return userInfo;
            }
        }

        public UUID AnonymousUserGUID
        {
            get
            {
                string guid = ( string ) Cache.Get( "AnonymousUserGUID" );

                if( string.IsNullOrEmpty( guid ) )
                    guid = ConfigurationManager.AppSettings["AnonymousUserGUID"];

                return new UUID( guid );
            }
        }

		public IEnumerable<CHAOS.Portal.Data.DTO.Group> Groups
		{
			get
			{
				using( PortalEntities db = new PortalEntities() )
				{
					return db.Group_Get( null, null, User.GUID.ToByteArray() ).ToDTO().ToList();
				}
			}
		}

		public IEnumerable<CHAOS.Portal.Data.DTO.SubscriptionInfo> Subscriptions
		{
			get
			{
				using( PortalEntities db = new PortalEntities() )
				{
					return db.SubscriptionInfo_Get( null, User.GUID.ToByteArray() ).ToDTO().ToList();
				}
			}
		}

        #endregion
        #region Construction

        public CallContext( ICache cache, IIndexManager indexManager, string sessionID )
        {
            Cache        = cache;
            IndexManager = indexManager;
            SessionGUID    = String.IsNullOrEmpty( sessionID ) ? (UUID) null : new UUID( sessionID );
        }

        #endregion
    }
}
