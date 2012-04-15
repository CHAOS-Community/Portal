using System.Collections.Generic;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
    [TestFixture]
    public abstract class TestBase
    {
        #region Properties

        public PortalApplication PortalApplication { get; set; }

        protected ICallContext CallContext { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            PortalApplication = new PortalApplication();
            CallContext = new CallContext(PortalApplication, new PortalRequest("MockExtension", "Test", new Dictionary<string, string>()), new PortalResponse());
        }
    }
}
