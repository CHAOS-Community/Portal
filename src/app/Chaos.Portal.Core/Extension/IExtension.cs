namespace Chaos.Portal.Core.Extension
{
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    public interface IExtension
    {
        IExtension WithPortalApplication(IPortalApplication portalApplication);
        IExtension WithPortalResponse(IPortalResponse response);
        IExtension WithPortalRequest(IPortalRequest request);

        IPortalResponse CallAction(IPortalRequest request);

        IPortalApplication PortalApplication { get; set; }
    }
}
