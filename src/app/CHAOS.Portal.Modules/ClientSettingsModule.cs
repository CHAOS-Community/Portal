using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Data.EF;
using Geckon;
using ClientSettings = CHAOS.Portal.DTO.Standard.ClientSettings;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class ClientSettingsModule : AModule
    {
        #region Properties

        private string ConnectionString { get; set; }

        private PortalEntities NewPortalEntities
        {
            get
            {
                return new PortalEntities( ConnectionString );
            }
        }

        #endregion
        #region Constructors

        public override void Initialize( string configuration )
        {
            ConnectionString = configuration;
        }

        #endregion
        #region Get

        [Datatype("ClientSettings","Get")]
        public ClientSettings Get( ICallContext callContext, UUID guid )
        {
            using( var db = NewPortalEntities )
            {
                return db.ClientSettings_Get( guid.ToByteArray() ).ToDTO().First();
            }
        }

        #endregion
    }
}
