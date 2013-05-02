namespace Chaos.Portal.Test
{
    using System;
    using System.Diagnostics;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Response;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Indexing.View;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Request;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class TestBase
    {
        #region Properties

        protected Mock<IPortalApplication> PortalApplication { get; set; }
        protected Mock<ICache>             Cache { get; set; }
        protected Mock<IViewManager>       ViewManager { get; set; }
        protected Mock<IPortalRepository>  PortalRepository { get; set; }
        protected Mock<ILogFactory>        LoggingFactory { get; set; }
        protected Mock<ILog>               Log { get; set; }
        protected Mock<IPortalRequest>     PortalRequest { get; set; }
        protected Mock<IPortalResponse>    PortalResponse { get; set; }
        protected Mock<IPortalHeader>      PortalHeader { get; set; }
        protected Mock<IExtension>         Extension { get; set; }

        #endregion
        #region Initialization

        [SetUp]
        public void SetUp()
        {
            Cache             = new Mock<ICache>();
            PortalRepository  = new Mock<IPortalRepository>();
            LoggingFactory    = new Mock<ILogFactory>();
            PortalRequest     = new Mock<IPortalRequest>();
            PortalResponse    = new Mock<IPortalResponse>();
            Extension         = new Mock<IExtension>();
            PortalHeader      = new Mock<IPortalHeader>();
            ViewManager       = new Mock<IViewManager>();
            PortalApplication = new Mock<IPortalApplication>();
            Log               = new Mock<ILog>();

            PortalApplication.SetupGet(p => p.PortalRepository).Returns(PortalRepository.Object);
            LoggingFactory.Setup(m => m.Create()).Returns(Log.Object);
            Log.Setup(m => m.WithLoglevel(It.IsAny<LogLevel>())).Returns(Log.Object);
            Log.Setup(m => m.WithName(It.IsAny<string>())).Returns(Log.Object);
            Log.Setup(m => m.WithSessionGuid(It.IsAny<Guid>())).Returns(Log.Object);
            Log.Setup(m => m.WithStopwatch(It.IsAny<Stopwatch>())).Returns(Log.Object);
        }
        
        #endregion
        #region Make

        protected PortalApplication Make_PortalApplication()
        {
            return new PortalApplication( Cache.Object, ViewManager.Object, PortalRepository.Object, LoggingFactory.Object );
        }

        #endregion
    }
}