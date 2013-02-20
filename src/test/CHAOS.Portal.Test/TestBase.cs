namespace Chaos.Portal.Test
{
    using Chaos.Portal.Cache;
    using Chaos.Portal.Data;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Index;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class TestBase
    {
        #region Properties

        protected Mock<ICache>            Cache { get; set; }
        protected Mock<IViewManager>      ViewManager { get; set; }
        protected Mock<IPortalRepository> PortalRepository { get; set; }
        protected Mock<ILogFactory>       LoggingFactory { get; set; }
        protected Mock<IPortalRequest>    PortalRequest { get; set; }
        protected Mock<IPortalResponse>   PortalResponse { get; set; }
        protected Mock<IPortalHeader>     PortalHeader { get; set; }
        protected Mock<IExtension>        Extension { get; set; }

        #endregion
        #region Initialization

        [SetUp]
        public void SetUp()
        {
            Cache            = new Mock<ICache>();
            PortalRepository = new Mock<IPortalRepository>();
            LoggingFactory   = new Mock<ILogFactory>();
            PortalRequest    = new Mock<IPortalRequest>();
            PortalResponse   = new Mock<IPortalResponse>();
            Extension        = new Mock<IExtension>();
            PortalHeader     = new Mock<IPortalHeader>();
            ViewManager      = new Mock<IViewManager>();
        }
        
        #endregion
    }
}