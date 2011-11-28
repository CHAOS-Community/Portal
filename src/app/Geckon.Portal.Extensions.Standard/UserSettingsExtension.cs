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
                UserSetting userSetting = db.UserSettings_Get( callContext.User.GUID, Guid.Parse(clientGUID)).FirstOrDefault();

                if( userSetting == null )
                {
                    PortalResult.GetModule("Geckon.Portal");
                    return;
                }

                PortalResult.GetModule("Geckon.Portal").AddResult( userSetting );
            }
        }

        [HttpPost]
        public void Create( CallContext callContext, string clientGUID, string settings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Create( callContext.User.GUID, Guid.Parse( clientGUID ), XElement.Parse( settings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                PortalResult.GetModule("Geckon.Portal").AddResult(db.UserSettings_Get( callContext.User.GUID, Guid.Parse(clientGUID)).First());
            }
        }

        [HttpPost]
        public void Update( CallContext callContext, string clientGUID, string newSettings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Update( callContext.User.GUID, Guid.Parse( clientGUID ), XElement.Parse( newSettings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                PortalResult.GetModule("Geckon.Portal").AddResult(new ScalarResult(result));
            }
        }

        public void Delete( CallContext callContext, string clientGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Delete( callContext.User.GUID, Guid.Parse( clientGUID ) );

                if( result == -10 )
                    throw new InvalidProtocolException();

                PortalResult.GetModule("Geckon.Portal").AddResult(new ScalarResult(result));
            }
        }
    }
}
