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

        public ContentResult Get( string sessionID )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.UserInfo user  = GetUserInfo( sessionID );
                Data.Dto.Group    group = Data.Dto.Group.Create( db.Group_Get( null, user.GUID ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }

        #endregion
        #region Create

        public ContentResult Create( string sessionID, string name )
        {
            Data.Dto.UserInfo user = GetUserInfo( sessionID );

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot create groups" );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.Group group = Data.Dto.Group.Create( db.Group_Insert( null, name ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "name", name ) );

            return GetContentResult( );
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID, string groupGUID )
        {
            Data.Dto.UserInfo user = GetUserInfo( sessionID );

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot delete groups" );

            using( PortalDataContext db = GetNewPortalDataContext() )
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

        public ContentResult Update( string sessionID, string groupGUID, string newName )
        {
            Data.Dto.UserInfo user = GetUserInfo( sessionID );

            //if( user.GUID == PortalContext.AnonymousUserGUID )
            //    throw new InsufficientPermissionsExcention( "Anonymous users cannot delete groups" );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                int? errorcode = 0;
                Data.Group group = db.Group_Update( newName, null, Guid.Parse( groupGUID ), user.ID, null, ref errorcode ).First();

                if( errorcode == -100 )
                    throw new InsufficientPermissionsExcention("User has insufficient permissions to update groups");

                ResultBuilder.Add( "Geckon.Portal",
                                   Data.Dto.Group.Create( group ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "groupGUID", groupGUID ) );

            return GetContentResult( );
        }

        #endregion
    }
}
