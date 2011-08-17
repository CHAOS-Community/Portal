using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Dto;

namespace Geckon.Portal.Extensions.Standard
{
    public class GroupExtension : AExtension
    {
        #region Constructor

        public GroupExtension(IPortalContext portalContext) : base(portalContext) { }
        public GroupExtension() : base() { }

        #endregion
        #region Get

        public ContentResult Get( string sessionID, string guid )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                Guid?             groupGuid = string.IsNullOrEmpty( guid ) ? (Guid?) null : Guid.Parse( guid );
                Data.Dto.UserInfo user      = CallContext.User;
                Data.Dto.Group    group     = Data.Dto.Group.Create( db.Group_Get( null, groupGuid, null, user.ID ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid ) );

            return GetContentResult();
        }

        #endregion
        #region Create

        public ContentResult Create( string sessionID, string name, int systemPermission )
        {
            Data.Dto.UserInfo user = CallContext.User;

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot create groups" );

            using (PortalDataContext db = PortalDataContext.Default())
            {
                int result = db.Group_Insert( null, name, systemPermission, user.ID );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "name", name ) );

            return GetContentResult( );
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID, string groupGUID )
        {
            Data.Dto.UserInfo user = CallContext.User;

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot delete groups" );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.Group_Delete( null, Guid.Parse( groupGUID ), user.ID, null );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention("User has insufficient permissions to delete groups");

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "groupGUID", groupGUID ) );

            return GetContentResult( );
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID, string groupGUID, string newName, int newSystemPermission )
        {
            Data.Dto.UserInfo user = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.Group_Update( newName, newSystemPermission, null, Guid.Parse( groupGUID ), user.ID, null );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention( "User does not have permission to update group" );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "groupGUID", groupGUID ),
                         new Parameter( "newName", newName ),
                         new Parameter( "newSystemPermission", newSystemPermission ) );

            return GetContentResult( );
        }

        #endregion
    }
}