namespace Chaos.Portal.Test.Extension
{
    using System;
    using System.Linq;

    using Chaos.Portal.Data;
    using Chaos.Portal.Extension;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ClientSettingsTest
    {
        protected Mock<ICallContext> CallContext { get; set; }

        protected Mock<IPortalApplication> PortalApplication { get; set; }

        protected Mock<IPortalRepository> PortalRepository { get; set; }
            
        [SetUp]
        public void SetUp()
        {
            CallContext       = new Mock<ICallContext>();
            PortalApplication = new Mock<IPortalApplication>();
            PortalRepository  = new Mock<IPortalRepository>();
        }

        [Test]
        public void Get_GivenGuid_CallUnderlyingPortalRepositoryAndReturnResult()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            PortalApplication.SetupGet(p => p.PortalRepository).Returns(PortalRepository.Object);
            PortalRepository.Setup(m => m.ClientSettingsGet(clientSettings.Guid)).Returns(new[] { clientSettings });

            var results = extension.Get(CallContext.Object, clientSettings.Guid).ToList();

            Assert.That(results[0], Is.SameAs(clientSettings));
        }

        private ClientSettings Make_ClientSettingsExtension()
        {
            return (ClientSettings)new ClientSettings().WithPortalApplication(PortalApplication.Object);
        }

        private Data.Dto.Standard.ClientSettings Make_ClientSettings()
        {
            return new Data.Dto.Standard.ClientSettings
                {
                    Guid        = new Guid("10000000-0000-0000-0000-000000000000"),
                    Name        = "test client",
                    Settings    = "some settings",
                    DateCreated = new DateTime(2000, 01, 01, 00, 00, 00)
                };
        }
    }
}