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
                Data.Dto.Session session = Data.Dto.Session.Create( db.Session_Insert( null, null, clientSettingsID ).First() );
            }
            
            return ConvertToContentResult( CallModules( new MethodQuery( "Create",
                                                                         "Session",
                                                                         new Parameter( "clientSettingsID", clientSettingsID ),
                                                                         new Parameter( "protocolVersion", protocolVersion ) ) ).Concat(  ) );
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
