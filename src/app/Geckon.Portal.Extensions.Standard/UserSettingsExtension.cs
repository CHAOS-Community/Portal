using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
          
namespace Geckon.Portal.Extensions.Standard
{
    public class UserSettingsExtension : AExtension
    {
        public void Get( CallContext callContext, string clientGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                UserSetting userSetting = db.UserSettings_Get( callContext.User.GUID, Guid.Parse(clientGUID)).FirstOrDefault();

                if( userSetting == null )
                {
                    PortalResult.GetModule("Geckon.Portal");
                    return;
                }

                moduleResult.AddResult( userSetting );
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public void Set( CallContext callContext, string clientGUID, string settings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                int result = db.UserSettings_Set( callContext.User.GUID, Guid.Parse( clientGUID ), XElement.Parse( settings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                moduleResult.AddResult( new ScalarResult( result ) );
            }
        }

        public void Delete( CallContext callContext, string clientGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                int result = db.UserSettings_Delete( callContext.User.GUID, Guid.Parse( clientGUID ) );

                if( result == -10 )
                    throw new InvalidProtocolException();

                moduleResult.AddResult( new ScalarResult( result ) );
            }
        }
    }
}
