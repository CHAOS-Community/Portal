using System.Data.Objects;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class SubscriptionModule : AModule
    {
        #region Properties

        private string ConnectionString { get; set; }

        private PortalEntities NewPortalEntities
        {
            get
            {
                return new PortalEntities( ConnectionString );
            }
        }

        #endregion
        #region Constructors

        public override void Initialize( string configuration )
        {
            ConnectionString = XDocument.Parse(configuration).Root.Attribute( "ConnectionString" ).Value;
        }

        #endregion
        #region Get

        [Datatype("Subscription","Get")]
        public DTO.Standard.SubscriptionInfo Get( ICallContext callContext, UUID guid )
        {
            var user = callContext.User;

            using( var db = NewPortalEntities )
            {
                var result = db.SubscriptionInfo_Get( guid.ToByteArray(), user.GUID.ToByteArray() ).ToDTO().FirstOrDefault();

                if( result == null )
                   throw new InsufficientPermissionsException( "User does not have sufficient permissions to access the subscription" );

                return result;
            }
        }

        #endregion
        #region Create

        [Datatype("Subscription", "Create")]
        public DTO.Standard.SubscriptionInfo Create( ICallContext callContext, string name )
        {
            var user = callContext.User;

            using( var db = NewPortalEntities )
            {
				var guid      = new UUID();
            	var errorCode = new ObjectParameter( "ErrorCode", 0 );

            	db.Subscription_Create( guid.ToByteArray(), name, user.GUID.ToByteArray(), errorCode );

                if( ((int) errorCode.Value) == -100 )
                    throw new InsufficientPermissionsException("User does not have sufficient permissions to access the subscription");

                var subscriptionInfo = db.SubscriptionInfo_Get( guid.ToByteArray(), user.GUID.ToByteArray() ).ToDTO().First();

                return subscriptionInfo;
            }
        }

        #endregion
        #region Delete

        [Datatype("Subscription", "Delete")]
        public ScalarResult Delete(ICallContext callContext, UUID guid)
        {
            var user      = callContext.User;
			var errorCode = new ObjectParameter("ErrorCode", 0);

            using( var db = NewPortalEntities )
            {
            	db.Subscription_Delete( guid.ToByteArray(), user.GUID.ToByteArray(), errorCode );
            }

        	if( ( (int) errorCode.Value ) == -100 )
                throw new InsufficientPermissionsException( "User does not have sufficient permissions to delete the subscription" );

			return new ScalarResult( 1 );
        }

        #endregion
        #region Update

        [Datatype("Subscription", "Update")]
        public ScalarResult Update(ICallContext callContext, UUID guid, string newName)
        {
            var user      = callContext.User;
			var errorCode = new ObjectParameter("ErrorCode", 0);

            using( var db = NewPortalEntities )
            {
            	db.Subscription_Update( guid.ToByteArray(), newName, user.GUID.ToByteArray(), errorCode );
            }

        	if( ( (int) errorCode.Value ) == -100 )
                throw new InsufficientPermissionsException( "User does not have sufficient permissions to access the subscription" );

            return new ScalarResult( 1 );
        }

        #endregion
    }
}
