namespace Chaos.Portal.Module
{
    using Chaos.Portal.Extension;

    public interface IModule
    {
        IExtension[] Extensions { get; }

        void Load(IPortalApplication portalApplication);
    }
}