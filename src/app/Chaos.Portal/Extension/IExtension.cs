using Chaos.Portal.Response;

namespace Chaos.Portal.Extension
{
    public interface IExtension
    {
        IExtension WithPortalApplication(IPortalApplication portalApplication);
        IExtension WithPortalResponse(IPortalResponse response);

        IPortalResponse CallAction(ICallContext callContext);

        IPortalApplication PortalApplication { get; set; }
    }
}
