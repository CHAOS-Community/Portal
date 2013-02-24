﻿namespace Chaos.Portal.Test
{
    using System;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Data;
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Indexing.View;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    using Moq;

    using NUnit.Framework;

    using ClientSettings = Chaos.Portal.Extension.ClientSettings;
    using Group = Chaos.Portal.Extension.Group;
    using Session = Chaos.Portal.Extension.Session;

    [TestFixture]
    public class TestBase
    {
        #region Properties

        protected Mock<ICallContext>       CallContext { get; set; }
        protected Mock<IPortalApplication> PortalApplication { get; set; }
        protected Mock<ICache>             Cache { get; set; }
        protected Mock<IViewManager>       ViewManager { get; set; }
        protected Mock<IPortalRepository>  PortalRepository { get; set; }
        protected Mock<ILogFactory>        LoggingFactory { get; set; }
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
            CallContext       = new Mock<ICallContext>();
            PortalApplication = new Mock<IPortalApplication>();

            PortalApplication.SetupGet(p => p.PortalRepository).Returns(PortalRepository.Object);
        }
        
        #endregion
        #region Make

        protected Data.Dto.Standard.ClientSettings Make_ClientSettings()
        {
            return new Data.Dto.Standard.ClientSettings
            {
                Guid = new Guid("10000000-0000-0000-0000-000000000000"),
                Name = "test client",
                Settings = "some settings",
                DateCreated = new DateTime(2000, 01, 01, 00, 00, 00)
            };
        }

        protected Data.Dto.Standard.Group Make_Group()
        {
            return new Data.Dto.Standard.Group
                {
                    Guid             = new Guid("01000000-0000-0000-0000-000000000010"),
                    Name             = "test group",
                    SystemPermission = 255,
                    DateCreated      = new DateTime(2000, 01, 01)
                };
        }

        protected IUserInfo Make_User()
        {
            return new UserInfo
                {
                    Guid = new Guid("00100000-0000-0000-0000-000000000100"),
                    Email = "test@test.test"
                };
        }

        protected Data.Dto.Standard.Session Make_Session()
        {
            return new Data.Dto.Standard.Session
                {
                    Guid        = new Guid("00001000-0000-0000-0000-000000010000"),
                    UserGuid    = Make_User().Guid,
                    DateCreated = new DateTime(2000, 01, 01)
                };
        }

        protected ClientSettings Make_ClientSettingsExtension()
        {
            return (ClientSettings)new ClientSettings().WithPortalApplication(PortalApplication.Object);
        }

        protected Group Make_GroupExtension()
        {
            return (Group)new Group().WithPortalApplication(PortalApplication.Object);
        }

        protected Session Make_SessionExtension()
        {
            return (Session)new Session().WithPortalApplication(PortalApplication.Object);
        }

        #endregion
    }
}