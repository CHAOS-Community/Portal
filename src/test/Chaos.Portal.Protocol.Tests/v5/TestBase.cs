﻿namespace Chaos.Portal.Protocol.Tests.v5
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Core;
    using Core.Cache;
    using Core.Data;
    using Core.Data.Model;
    using Core.Extension;
    using Core.Indexing.View;
    using Core.Logging;
    using Core.Request;
    using Core.Response;
    using Portal.v5.Extension;

    using Moq;

    using NUnit.Framework;

    using ClientSettings = Portal.v5.Extension.ClientSettings;
    using Group = Portal.v5.Extension.Group;
    using Session = Portal.v5.Extension.Session;

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
            PortalRequest.SetupGet(p => p.User).Returns(Make_User());
            PortalRequest.SetupGet(p => p.Session).Returns(Make_Session());
            PortalRequest.SetupGet(p => p.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });
            LoggingFactory.Setup(m => m.Create()).Returns(Log.Object);
            Log.Setup(m => m.WithLoglevel(It.IsAny<LogLevel>())).Returns(Log.Object);
            Log.Setup(m => m.WithName(It.IsAny<string>())).Returns(Log.Object);
            Log.Setup(m => m.WithSessionGuid(It.IsAny<Guid>())).Returns(Log.Object);
            Log.Setup(m => m.WithStopwatch(It.IsAny<Stopwatch>())).Returns(Log.Object);
        }
        
        #endregion
        #region Make

        protected Core.Data.Model.ClientSettings Make_ClientSettings()
        {
            return new Core.Data.Model.ClientSettings
            {
                Guid        = new Guid("10000000-0000-0000-0000-000000000000"),
                Name        = "test client",
                Settings    = "some settings",
                DateCreated = new DateTime(2000, 01, 01, 00, 00, 00)
            };
        }

        protected Core.Data.Model.Group Make_Group()
        {
            return new Core.Data.Model.Group
                {
                    Guid             = new Guid("01000000-0000-0000-0000-000000000010"),
                    Name             = "test group",
                    SystemPermission = 255,
                    DateCreated      = new DateTime(2000, 01, 01)
                };
        }

        protected UserInfo Make_User()
        {
            return new UserInfo
                {
                    Guid = new Guid("00100000-0000-0000-0000-000000000100"),
                    Email = "test@test.test",
                    SystemPermissions = (uint?)SystemPermissons.All
                };
        }

        protected Core.Data.Model.Session Make_Session()
        {
            return new Core.Data.Model.Session
                {
                    Guid        = new Guid("00001000-0000-0000-0000-000000010000"),
                    UserGuid    = Make_User().Guid,
                    DateCreated = new DateTime(2000, 01, 01)
                };
        }

        protected ClientSettings Make_ClientSettingsExtension()
        {
            return (ClientSettings)new ClientSettings(PortalApplication.Object).WithPortalRequest(PortalRequest.Object)
                                                                               .WithPortalResponse(PortalResponse.Object);
        }

        protected Group Make_GroupExtension()
        {
            return (Group)new Group(PortalApplication.Object).WithPortalRequest(PortalRequest.Object)
                                                             .WithPortalResponse(PortalResponse.Object);
        }

        protected Session Make_SessionExtension()
        {
            return (Session)new Session(PortalApplication.Object).WithPortalRequest(PortalRequest.Object)
                                                                 .WithPortalResponse(PortalResponse.Object);
        }

        protected Subscription Make_SubscriptionExtension()
        {
            return (Subscription)new Subscription(PortalApplication.Object).WithPortalRequest(PortalRequest.Object)
                                                                           .WithPortalResponse(PortalResponse.Object);
        }

        protected PortalApplication Make_PortalApplication()
        {
            return new PortalApplication( Cache.Object, ViewManager.Object, PortalRepository.Object, LoggingFactory.Object );
        }

        protected Portal.v5.Extension.User Make_UserExtension()
        {
            return (Portal.v5.Extension.User)new Portal.v5.Extension.User(PortalApplication.Object).WithPortalRequest(PortalRequest.Object).WithPortalResponse(PortalResponse.Object);
        }

        #endregion
    }
}