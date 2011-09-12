using System;
using System.Linq;
using Geckon.Data;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class SessionExtension : AExtension
    {
        #region Get

        public void Get( string sessionID )
        {
            Session session = PortalContext.Cache.Get<Session>( string.Format( "[Session:sid={0}]", sessionID ) );
            int? totalCount = 1;

            if( session == null )
            {
                using( PortalDataContext db = PortalDataContext.Default() )
                {
                    
                    session = db.Session_Get( Guid.Parse( sessionID ), null, 0, null, ref totalCount ).First();

                    PortalContext.Cache.Put( string.Format("[Session:sid={0}]", sessionID), 
                                             session.ToXML().OuterXml, 
                                             new TimeSpan( 0, 1, 0 ) );
                }
            }

            ResultBuilder.Add( "Geckon.Portal",
                                session,
                                new NameValue( "TotalCount", totalCount.ToString() ) );
        } 

        #endregion
        #region Create

        public void Create(  int protocolVersion )
        {
            // TODO: Check protocol version
            // TODO: Add Module filtering

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal", 
                                   db.Session_Insert( null, 
                                                      PortalContext.AnonymousUserGUID ).First() );
            }
        }

        #endregion
        #region Update

        public void Update( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   db.Session_Update( null, null, Guid.Parse( sessionID ), null ).First() );
            }
        }

        #endregion
        #region Delete

        public void Delete( string sessionID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( db.Session_Delete( Guid.Parse( sessionID ), null ) ) );
            }
        }

        #endregion
    }
}
