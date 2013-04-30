using Chaos.Portal.Response;

namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Core.Response;
    using Chaos.Portal.Request;

    public interface IExtension
    {
        IExtension WithPortalApplication(IPortalApplication portalApplication);
        IExtension WithPortalResponse(IPortalResponse response);
        IExtension WithPortalRequest(IPortalRequest request);

        IPortalResponse CallAction(IPortalRequest request);

        IPortalApplication PortalApplication { get; set; }
    }
}
