using System;
using System.Linq;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class ClientSettingsExtension : AExtension
    {
        #region GET

        public void Get( string sessionID, string guid )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ClientSetting client = db.ClientSettings_Get( Guid.Parse( guid ) ).First();

                PortalResult.GetModule("Geckon.Portal").AddResult( client );
            }
        }

        #endregion
    }
}