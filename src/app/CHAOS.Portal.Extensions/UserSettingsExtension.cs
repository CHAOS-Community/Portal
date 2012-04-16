using System;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using CHAOS.Portal.Data.DTO;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
          
namespace Geckon.Portal.Extensions.Standard
{
    public class UserSettingsExtension : AExtension
    {
        public void Get( CallContext callContext, string clientGUID )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                CHAOS.Portal.Data.DTO.UserSettings userSetting = db.UserSettings_Get( new UUID( clientGUID ).ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().FirstOrDefault();

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
            using( PortalEntities db = new PortalEntities() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                int result = db.UserSettings_Set( new UUID( clientGUID ).ToByteArray(), callContext.User.GUID.ToByteArray(), settings );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                moduleResult.AddResult( new ScalarResult( result ) );
            }
        }

        public void Delete( CallContext callContext, string clientGUID )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                var moduleResult = PortalResult.GetModule("Geckon.Portal");

                int result = db.UserSettings_Delete( callContext.User.GUID.ToByteArray(), new UUID( clientGUID ).ToByteArray() );

                if( result == -10 )
                    throw new InvalidProtocolException();

                moduleResult.AddResult( new ScalarResult( result ) );
            }
        }
    }
}
