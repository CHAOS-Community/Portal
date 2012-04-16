using System.Data.Objects;
using System.Linq;
using CHAOS.Portal.Data.DTO;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using SubscriptionInfo = CHAOS.Portal.Data.DTO.SubscriptionInfo;
using UserInfo = CHAOS.Portal.Data.DTO.UserInfo;

namespace Geckon.Portal.Extensions.Standard
{
    public class SubscriptionExtension : AExtension
    {
        #region Get

        public void Get( CallContext callContext, string guid )
        {
            UserInfo         user   = callContext.User;
            SubscriptionInfo result = null;

            using( PortalEntities db = new PortalEntities() )
            {
                result = db.SubscriptionInfo_Get( new UUID( guid ).ToByteArray(), user.GUID.ToByteArray()).ToDTO().FirstOrDefault();
            }

            if( result == null )
                throw new InsufficientPermissionsException( "User does not have sufficient permissions to access the subscription" );

            PortalResult.GetModule("Geckon.Portal").AddResult( result );
        }

        #endregion
        #region Create

        public void Create( CallContext callContext, string name )
        {
            UserInfo user = callContext.User;

            using( PortalEntities db = new PortalEntities() )
            {
				UUID            guid      = new UUID();
            	ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

            	db.Subscription_Create( guid.ToByteArray(), name, user.GUID.ToByteArray(), errorCode );

                if( ((int) errorCode.Value) == -100 )
                    throw new InsufficientPermissionsException("User does not have sufficient permissions to access the subscription");

                SubscriptionInfo subscriptionInfo = db.SubscriptionInfo_Get( guid.ToByteArray(), user.GUID.ToByteArray() ).ToDTO().First();

                PortalResult.GetModule("Geckon.Portal").AddResult( subscriptionInfo );
            }
        }

        #endregion
        #region Delete

        public void Delete( CallContext callContext, string guid )
        {
            UserInfo        user      = callContext.User;
			ObjectParameter errorCode = new ObjectParameter("ErrorCode", 0);

            using( PortalEntities db = new PortalEntities() )
            {
            	db.Subscription_Delete( new UUID( guid ).ToByteArray(), user.GUID.ToByteArray(), errorCode );
            }

        	if( ( (int) errorCode.Value ) == -100 )
                throw new InsufficientPermissionsException( "User does not have sufficient permissions to delete the subscription" );

			PortalResult.GetModule("Geckon.Portal").AddResult( new ScalarResult( 1 ) );
        }

        #endregion
        #region Update

        public void Update( CallContext callContext, string guid, string newName )
        {
            UserInfo        user      = callContext.User;
			ObjectParameter errorCode = new ObjectParameter("ErrorCode", 0);

            using( PortalEntities db = new PortalEntities() )
            {
            	db.Subscription_Update( new UUID( guid ).ToByteArray(), newName, user.GUID.ToByteArray(), errorCode );
            }

        	if( ( (int) errorCode.Value ) == -100 )
                throw new InsufficientPermissionsException( "User does not have sufficient permissions to access the subscription" );

            PortalResult.GetModule("Geckon.Portal").AddResult( new ScalarResult( 1 ) );
        }

        #endregion
    }
}