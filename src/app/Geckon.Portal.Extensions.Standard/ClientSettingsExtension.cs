using System.Linq;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Standard.Extension;
using ClientSettings = CHAOS.Portal.Data.DTO.ClientSettings;

namespace Geckon.Portal.Extensions.Standard
{
    public class ClientSettingsExtension : AExtension
    {
        #region GET

        public void Get( CallContext callContext, string guid )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                ClientSettings client = db.ClientSettings_Get( new UUID( guid ).ToByteArray() ).ToDTO().First();

                PortalResult.GetModule("Geckon.Portal").AddResult(client);
            }
        }

        #endregion
    }
}