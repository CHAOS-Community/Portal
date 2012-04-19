using System.Linq;
using System.Data.Objects;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;
using Group = CHAOS.Portal.DTO.Standard.Group;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class GroupModule : AModule
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
            ConnectionString = configuration;
        }

        #endregion
        #region Get

        [Datatype("Group","Get")]
        public Group Get( ICallContext callContext, UUID guid )
        {
            using( var db = NewPortalEntities )
            {
                var user  = callContext.User;
                var group = db.Group_Get( guid != null ? guid.ToByteArray() : null, null, user.GUID.ToByteArray() ).ToDTO().First();

                return group;
            }
        }

        #endregion
        #region Create

        [Datatype("Group", "Create")]
        public Group Create( ICallContext callContext, string name, int systemPermission )
        {
            var user = callContext.User;
            
            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				var guid      = new UUID();
				var errorCode = new ObjectParameter( "ErrorCode", 0 );

            	db.Group_Create( guid.ToByteArray(), name, user.GUID.ToByteArray(), systemPermission, errorCode );

				if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
                    throw new UnhandledException("Group_Create was rolled back");

                return db.Group_Get( guid.ToByteArray(), null, user.GUID.ToByteArray() ).ToDTO().First();
            }
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, UUID guid )
        {
            var user = callContext.User;

            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				var errorCode = new ObjectParameter( "ErrorCode", 0 );

                db.Group_Delete( guid.ToByteArray(), user.GUID.ToByteArray(), errorCode );

                if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
					throw new UnhandledException("Group_Delete was rolled back");

                return new ScalarResult( 1 );
            }
        }

        #endregion
        #region Update

        public ScalarResult Update( ICallContext callContext, UUID guid, string newName, int newSystemPermission )
        {
            var user = callContext.User;

            using( PortalEntities db = new PortalEntities() )
            {
				int result = db.Group_Update( newName, newSystemPermission, guid.ToByteArray(), user.GUID.ToByteArray() ).First().Value;

                if( result == -100 )
                    throw new InsufficientPermissionsException( "User does not have permission to update group" );

                return new ScalarResult( 1 );
            }
        }

        #endregion
    }
}
