namespace Chaos.Portal.Test.Extension
{
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class ClientSettingsTest : TestBase
    {
        [Test]
        public void Get_GivenGuid_CallUnderlyingPortalRepositoryAndReturnResult()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            PortalRepository.Setup(m => m.ClientSettingsGet(clientSettings.Guid)).Returns(new[] { clientSettings });

            var results = extension.Get(CallContext.Object, clientSettings.Guid).ToList();

            Assert.That(results[0], Is.SameAs(clientSettings));
        }

        [Test]
        public void Set_MakeCreateANewClientSetting_ReturnsOne()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            PortalRepository.Setup(m => m.ClientSettingsSet(clientSettings.Guid, clientSettings.Name, clientSettings.Settings)).Returns(1);

            var results = extension.Set(CallContext.Object, clientSettings.Guid, clientSettings.Name, clientSettings.Settings);

            Assert.That(results, Is.EqualTo(1));
        }
    }
}