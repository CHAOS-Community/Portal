using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Data;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

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
            Data.Dto.Session session = PortalContext.Cache.Get<Data.Dto.Session>( string.Format( "[Session:sid={0}]", sessionID ) );
            int? totalCount = 1;

            if( session == null )
            {
                using( PortalDataContext db = PortalDataContext.Default() )
                {
                    
                    session = Data.Dto.Session.Create( db.Session_Get( Guid.Parse( sessionID ), null, null, 0, null, ref totalCount ).First() );

                    PortalContext.Cache.Put( string.Format( "[Session:sid={0}]", sessionID ), 
                                             session.ToXML().OuterXml, 
                                             new TimeSpan( 0, 1, 0 ) );
                }
            }

            ResultBuilder.Add( "Geckon.Portal",
                                session,
                                new NameValue( "TotalCount", totalCount.ToString() ) );

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        } 

        #endregion
        #region Create

        public ContentResult Create( int clientSettingsID, int protocolVersion )
        {
            // TODO: Check protocol version
            // TODO: Add Module filtering

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal", 
                                   Data.Dto.Session.Create( db.Session_Insert( null, 
                                                                               PortalContext.AnonymousUserGUID, 
                                                                               clientSettingsID ).First() ) );
            }

            CallModules( new Parameter( "clientSettingsID", clientSettingsID ),
                         new Parameter( "protocolVersion", protocolVersion ) );

            return GetContentResult( );
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   Data.Dto.Session.Create( db.Session_Update( null, null, null, Guid.Parse( sessionID ), null, null  ).First() ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   new Data.Dto.ScalarResult( db.Session_Delete( Guid.Parse( sessionID ), null, null ) ) );
            }

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }

        #endregion
    }
}
