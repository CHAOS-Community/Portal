using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Data;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class SessionExtension : AExtension
    {
        #region Get

        public ContentResult Get( string sessionID )
        {
            Session session = PortalContext.Cache.Get<Session>( string.Format( "[Session:sid={0}]", sessionID ) );
            int? totalCount = 1;

            if( session == null )
            {
                using( PortalDataContext db = PortalDataContext.Default() )
                {
                    
                    session = db.Session_Get( Guid.Parse( sessionID ), null, null, 0, null, ref totalCount ).First();

                    PortalContext.Cache.Put( string.Format("[Session:sid={0}]", sessionID), 
                                             session.ToXML().OuterXml, 
                                             new TimeSpan( 0, 1, 0 ) );
                }
            }

            ResultBuilder.Add( "Geckon.Portal",
                                session,
                                new NameValue( "TotalCount", totalCount.ToString() ) );

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
                                   db.Session_Insert( null, 
                                                      PortalContext.AnonymousUserGUID, 
                                                      clientSettingsID ).First() );
            }

            return GetContentResult( );
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   db.Session_Update( null, null, null, Guid.Parse( sessionID ), null, null  ).First() );
            }

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( db.Session_Delete( Guid.Parse( sessionID ), null, null ) ) );
            }

            return GetContentResult();
        }

        #endregion
    }
}
