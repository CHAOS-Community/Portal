using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core
{
    public class PortalApplication
    {
        #region Properties

        public ParameterBindings Bindings { get; set; }

        #endregion
        #region Constructors

        public PortalApplication()
        {
            Bindings = new ParameterBindings();
        }

        #endregion
        #region Business Logic

        public void ProcessRequest( IPortalRequest request )
        {
            ICallContext callContext = new CallContext( this, request );
        }

        #endregion
    }
}
