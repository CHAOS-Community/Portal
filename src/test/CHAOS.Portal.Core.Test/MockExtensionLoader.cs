using CHAOS.Portal.Core.Extension;

namespace CHAOS.Portal.Core.Test
{
    public class MockExtensionLoader : IExtensionLoader
    {
        public IExtension CreateInstance()
        {
            return new MockExtension();
        }
    }
}
