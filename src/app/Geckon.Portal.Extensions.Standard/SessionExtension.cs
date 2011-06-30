using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard;
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
            return ConvertToContentResult( CallModules( new MethodQuery( "Get",
                                                                         "Session",
                                                                         new Parameter( "sessionID", sessionID ) ) ) );
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
            
            //ResultBuilder.Add( CallModules( new MethodQuery( "Create",
            //                                                 "Session",
            //                                                 new Parameter( "clientSettingsID", clientSettingsID ),
            //                                                 new Parameter( "protocolVersion", protocolVersion ) ) ) );

            return ConvertToContentResult( );
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID )
        {
            return ConvertToContentResult( CallModules( new MethodQuery( "Update",
                                                                         "Session",
                                                                         new Parameter( "sessionID", sessionID ) ) ) );
        }

        #endregion
        #region Delete

        public ContentResult Delete( string sessionID )
        {
            return ConvertToContentResult( CallModules( new MethodQuery( "Delete",
                                                                         "Session",
                                                                         new Parameter( "sessionID", sessionID ) ) ) );
        }

        #endregion
    }
}
