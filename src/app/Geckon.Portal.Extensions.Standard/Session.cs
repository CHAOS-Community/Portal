using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard;

namespace Geckon.Portal.Extensions.Standard
{
    public class Session : AExtension
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

        public ContentResult Create( int repositoryID, int clientSettingsID, int protocolVersion )
        {
            return ConvertToContentResult( CallModules( new MethodQuery( "Create",
                                                                         "Session",
                                                                         new Parameter( "repositoryID", repositoryID ),
                                                                         new Parameter( "clientSettingsID", clientSettingsID ),
                                                                         new Parameter( "protocolVersion", protocolVersion ) ) ) );
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
