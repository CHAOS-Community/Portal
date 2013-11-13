using System;
using CHAOS;
using CHAOS.Extensions;
using Chaos.Portal.v5.Extension;
using Moq;
using NUnit.Framework;

namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    [TestFixture]
    public class UserSettingsTest : TestBase
    {
        [Test]
        public void Get_ValidClientGuid_ReturnClientSettings()
        {
            var extension = Make_v5UserSettingsExtension();
            var userSettings = Make_UserSettings();
            var uuid = userSettings.ClientSettingGuid.ToUUID();
            PortalRepository.Setup(m => m.UserSettingsGet(userSettings.ClientSettingGuid, userSettings.UserGuid)).Returns(userSettings);

            var result = extension.Get(uuid);

            Assert.That(result.ClientSettingGuid.ToString(), Is.EqualTo(uuid.ToString()));
            Assert.That(result.ClientSettingGuid.ToByteArray(), Is.EqualTo(uuid.ToByteArray()));
        }

        [Test]
        public void Set_ValidClientGuid_CallRepositoryAndReturnOne()
        {
            var extension = Make_v5UserSettingsExtension();
            var userSettings = Make_UserSettings();
            PortalRepository.Setup(m => m.UserSettingsSet(userSettings.ClientSettingGuid, userSettings.UserGuid, userSettings.Settings)).Returns(1);

            var result = extension.Set(userSettings.ClientSettingGuid.ToUUID(), userSettings.Settings);

            Assert.That(result, Is.EqualTo(1));
            PortalRepository.VerifyAll();
        }

        [Test]
        public void Delete_ValidClientGuid_CallRepositoryAndReturnOne()
        {
            var extension = Make_v5UserSettingsExtension();
            var userSettings = Make_UserSettings();
            PortalRepository.Setup(m => m.UserSettingsDelete(userSettings.ClientSettingGuid, userSettings.UserGuid)).Returns(1);

            var result = extension.Delete(userSettings.ClientSettingGuid.ToUUID());

            Assert.That(result, Is.EqualTo(1));
            PortalRepository.VerifyAll();
        }

        private Core.Data.Model.UserSettings Make_UserSettings()
        {
            return new Core.Data.Model.UserSettings()
            {
                ClientSettingGuid = new Guid("10000000-0000-0000-0000-000000000001"),
                UserGuid = Make_Session().UserGuid,
                Settings = "some settings"
            };
        }

        private UserSettings Make_v5UserSettingsExtension()
        {
            return (UserSettings)new UserSettings(PortalApplication.Object).WithPortalRequest(PortalRequest.Object);
        }
    }
}