namespace Chaos.Portal.Protocol.Tests
{
    using System;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Extension;

    public class ExtensionMock : AExtension
    {
        public ExtensionMock(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        public int test()
        {
            return 1;
        }

        public int error()
        {
            throw new ArgumentException();
        }
    }
}