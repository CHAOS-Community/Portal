using System;
using System.Linq;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Extensions.Standard
{
    public class SessionExtension : AExtension
    {
        #region Get

        public void Get(CallContext callContext)
        {
            IModuleResult module = PortalResult.GetModule("Geckon.Portal");

            Session session = null;// PortalContext.Cache.Get<Session>( string.Format( "[Session:sid={0}]", sessionID ) );
            int? totalCount = 1;

            if (session == null)
            {
                using (PortalDataContext db = PortalDataContext.Default())
                {

                    session = db.Session_Get( callContext.SessionID, null, 0, null, ref totalCount).First();

                    //PortalContext.Cache.Put( string.Format("[Session:sid={0}]", sessionID), 
                    //                         session.ToXML().OuterXml, 
                    //                         new TimeSpan( 0, 1, 0 ) );
                }
            }

            
            module.AddResult(session);

            //new KeyValuePair<string, object>( "TotalCount", totalCount.ToString() ) );
        } 

        #endregion
        #region Create

        public void Create( CallContext callContext, int protocolVersion )
        {
            // TODO: Check protocol version
            // TODO: Add Module filtering

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                PortalResult.GetModule( "Geckon.Portal" ).AddResult( db.Session_Insert( null, 
                                                                                        callContext.AnonymousUserGUID ).First() );
            }
        }

        #endregion
        #region Update

        public void Update( CallContext callContext )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                PortalResult.GetModule( "Geckon.Portal" ).AddResult( db.Session_Update( null, 
                                                                                        null, 
                                                                                        callContext.SessionID, 
                                                                                        null ).First() );
            }
        }

        #endregion
        #region Delete

        public void Delete( CallContext callContext )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                PortalResult.GetModule( "Geckon.Portal" ).AddResult( new ScalarResult( db.Session_Delete( callContext.SessionID, 
                                                                                                          null ) ) );
            }
        }

        #endregion
    }
}
