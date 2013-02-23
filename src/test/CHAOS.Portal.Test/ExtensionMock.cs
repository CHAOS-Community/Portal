namespace Chaos.Portal.Test
{
    using Chaos.Portal.Extension;
    using Chaos.Portal.Response;

    public class ExtensionMock : IExtension
    {
        public IExtension WithPortalApplication(IPortalApplication portalApplication)
        {
            throw new System.NotImplementedException();
        }

        public IExtension WithConfiguration(string configuration)
        {
            throw new System.NotImplementedException();
        }

        public IPortalResponse CallAction(ICallContext callContext)
        {
            throw new System.NotImplementedException();
        }

        public IPortalApplication PortalApplication { get; set; }
    }
}