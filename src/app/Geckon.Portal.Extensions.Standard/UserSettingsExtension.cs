using System;
using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserSettingsExtension : AExtension
    {
        #region GET

        public void Get( CallContext callContext, string clientGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                PortalResult.GetModule("Geckon.Portal").AddResult(db.UserSettings_Get( callContext.User.ID, null, Guid.Parse(clientGUID)).First());
            }
        }

        #endregion
        #region CREATE

        public void Create( CallContext callContext, string clientGUID, string settings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Create( callContext.User.ID, null, Guid.Parse( clientGUID ), XElement.Parse( settings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                PortalResult.GetModule("Geckon.Portal").AddResult(db.UserSettings_Get( callContext.User.ID, null, Guid.Parse(clientGUID)).First());
            }
        }

        #endregion
        #region UPDATE

        public void Update( CallContext callContext, string clientGUID, string newSettings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Update( callContext.User.ID, null, Guid.Parse( clientGUID ), XElement.Parse( newSettings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                PortalResult.GetModule("Geckon.Portal").AddResult(new ScalarResult(result));
            }
        }

        #endregion
        #region DELETE

        public void Delete( CallContext callContext, string clientGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Delete( callContext.User.ID, null, Guid.Parse( clientGUID ) );

                if( result == -10 )
                    throw new InvalidProtocolException();

                PortalResult.GetModule("Geckon.Portal").AddResult(new ScalarResult(result));
            }
        }

        #endregion
    }
}
