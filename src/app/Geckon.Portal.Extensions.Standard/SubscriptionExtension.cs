using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Dto;
using Subscription = Geckon.Portal.Data.Subscription;
using UserInfo = Geckon.Portal.Data.Dto.UserInfo;

namespace Geckon.Portal.Extensions.Standard
{
    public class SubscriptionExtension : AExtension
    {
        #region Construction

        public SubscriptionExtension( IPortalContext portalContext ) : base( portalContext ) { }
        public SubscriptionExtension() : base() { }

        #endregion
        #region Get

        public ContentResult Get( string sessionID, string guid )
        {
            UserInfo     user   = GetUserInfo( sessionID );
            Subscription result = null;

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                result = db.Subscription_Get(null, Guid.Parse(guid), null, user.ID).FirstOrDefault();
            }

            if( result == null )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

            Data.Dto.Subscription subscription = Data.Dto.Subscription.Create( result );

            ResultBuilder.Add( "Geckon.Portal",
                                   subscription );

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid           ) );

            return GetContentResult();
        }

        #endregion
        #region Create

        public ContentResult Create( string sessionID, string name )
        {
            UserInfo user   = GetUserInfo( sessionID );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                int result = db.Subscription_Insert( Guid.NewGuid(), name, user.ID );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

                Data.Dto.Subscription subscription = Data.Dto.Subscription.Create( db.Subscription_Get( result, null, null, user.ID ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   subscription );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "name", name           ) );

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID, string guid )
        {
            UserInfo user   = GetUserInfo( sessionID );
            int      result = 0;

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                result = db.Subscription_Delete( null, Guid.Parse( guid ), user.ID );
            }

            if( result == -100 )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to delete the subscription" );

            ResultBuilder.Add( "Geckon.Portal",
                               new ScalarResult( result ) );

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid           ) );

            return GetContentResult();
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID, string guid, string newName )
        {
            UserInfo user      = GetUserInfo( sessionID );
            int      result    = 0;

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                result = db.Subscription_Update( null, Guid.Parse( guid ), newName, user.ID );
            }

            if( result == -100 )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

            ResultBuilder.Add( "Geckon.Portal",
                               new ScalarResult( result ) );

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid           ) );

            return GetContentResult();
        }

        #endregion
    }
}
