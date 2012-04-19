using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Extension.Standard
{
    /// <summary>
    /// The Default Extension serves as a fall-through extension, it will call any module that matches the requested Extension and Action
    /// </summary>
    public class DefaultExtension : AExtension
    {
        #region Business Logic

        public override void CallAction( ICallContext callContext )
        {
            if( !callContext.PortalApplication.LoadedModules.ContainsKey( callContext.PortalRequest.Extension ) )
                throw new ExtensionMissingException( "The requested Extension wasn't found in any loaded assembly" );

            // TODO: Make module execution parallel
            foreach( IModule module in callContext.PortalApplication.LoadedModules[ callContext.PortalRequest.Extension ] )
            {
                module.CallAction( callContext );
            }
        }

        #endregion
    }
}
