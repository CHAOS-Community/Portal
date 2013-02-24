using Chaos.Portal.Response;

namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Data;

    public interface IExtension
    {
        IExtension WithPortalApplication(IPortalApplication portalApplication);
        //todo: remove WithConfiguration
        IExtension WithConfiguration(string configuration);

        IPortalResponse CallAction(ICallContext callContext);

        IPortalApplication PortalApplication { get; set; }
    }
}
