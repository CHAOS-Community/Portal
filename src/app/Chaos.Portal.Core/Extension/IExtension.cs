namespace Chaos.Portal.Core.Extension
{
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    public interface IExtension
    {
        IPortalResponse CallAction(IPortalRequest request);

        IExtension WithPortalResponse(IPortalResponse response);

        IExtension WithPortalRequest(IPortalRequest request);
    }
}
