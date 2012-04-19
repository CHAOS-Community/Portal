using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Data.EF;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class UserModule : AModule
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
        public DTO.Standard.UserInfo Get( ICallContext callContext )
        {
            return callContext.User;
        }

        #endregion
    }
}
