namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    using Chaos.Portal.Core.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class ClientSettingsTest : TestBase
    {
        [Test]
        public void Get_GivenGuid_CallUnderlyingPortalRepositoryAndReturnResult()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            PortalRepository.Setup(m => m.ClientSettingsGet(clientSettings.Guid)).Returns(clientSettings);

            var results = extension.Get(clientSettings.Guid);

            Assert.That(results.GUID, Is.EqualTo(clientSettings.Guid));
        }

        [Test]
        public void Set_CreateANewClientSetting_ReturnsOne()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            PortalRepository.Setup(m => m.ClientSettingsSet(clientSettings.Guid, clientSettings.Name, clientSettings.Settings)).Returns(1);

            var results = extension.Set(clientSettings.Guid, clientSettings.Name, clientSettings.Settings);

            Assert.That(results, Is.EqualTo(1));
        }
        
        [Test, ExpectedException(typeof(InsufficientPermissionsException))]
        public void Set_WithoutPermission_ThrowsException()
        {
            var extension      = Make_ClientSettingsExtension();
            var clientSettings = Make_ClientSettings();
            var user           = Make_User();
            user.SystemPermissions = 0;
            PortalRequest.SetupGet(p => p.User).Returns(user);

            extension.Set(clientSettings.Guid, clientSettings.Name, clientSettings.Settings);
        }
    }
}