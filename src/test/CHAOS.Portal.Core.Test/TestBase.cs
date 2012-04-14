using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
    [TestFixture]
    public abstract class TestBase
    {
        #region Properties

        public PortalApplication PortalApplication { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            PortalApplication = new PortalApplication();
        }
    }
}
