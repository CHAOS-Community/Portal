﻿using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Dto;

namespace Geckon.Portal.Extensions.Standard
{
    public class SessionExtension : AExtension
    {
        #region Constructor

        public SessionExtension()
        {
        }

        public SessionExtension(IPortalContext context)
            : base(context)
        {
        }

        #endregion
        #region Get

        public ContentResult Get( string sessionID )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                ResultBuilder.Add( "Geckon.Portal", 
                                   Data.Dto.Session.Create( db.Session_Get( Guid.Parse( sessionID ),
                                                                                        null,
                                                                                        null ).First() ) );
            }
            
            CallModules( new MethodQuery( "Get",
                                          "Session",
                                          new Parameter( "sessionID", sessionID ) ) );

            return GetContentResult();
        } 

        #endregion
        #region Create

        public ContentResult Create( int clientSettingsID, int protocolVersion )
        {
            // TODO: Check protocol version
            // TODO: Add Module filtering

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                ResultBuilder.Add( "Geckon.Portal", 
                                   Data.Dto.Session.Create( db.Session_Insert( null, 
                                                                               PortalContext.AnonymousUserGUID, 
                                                                               clientSettingsID ).First() ) );
            }

            CallModules( new MethodQuery( "Create",
                                          "Session",
                                          new Parameter( "clientSettingsID", clientSettingsID ),
                                          new Parameter( "protocolVersion", protocolVersion ) ) );

            return GetContentResult( );
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   Data.Dto.Session.Create( db.Session_Update( null, null, null, Guid.Parse( sessionID ), null, null  ).First() ) );
            }

            CallModules( new MethodQuery( "Update",
                                          "Session",
                                          new Parameter( "sessionID", sessionID ) ) );

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( db.Session_Delete( Guid.Parse( sessionID ), null, null ) ) );
            }

            CallModules( new MethodQuery( "Delete",
                                          "Session",
                                          new Parameter( "sessionID", sessionID ) ) );

            return GetContentResult();
        }

        #endregion
    }
}
