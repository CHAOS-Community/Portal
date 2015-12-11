namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    using System;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SessionTest : TestBase
    {
        [Test]
        public void Get_CurrentSession_ReturnCurrentSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();

            var actual = extension.Get();

            Assert.That(actual.Guid, Is.EqualTo(expected.Guid));
        }
        
        [Test]
        public void Create_NewSession_ReturnSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();
            PortalRepository.Setup(m => m.SessionCreate(It.IsAny<Guid>())).Returns(expected);

            var actual = extension.Create();

            Assert.That(actual.Guid, Is.EqualTo(expected.Guid));
            Assert.That(actual.UserGuid, Is.EqualTo(expected.UserGuid));
        }
        
        [Test]
        public void Update_RenewCurrentSeesion_ReturnSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();
            var user      = Make_User();
            PortalRepository.Setup(m => m.SessionUpdate(expected.Guid, user.Guid)).Returns(expected);

            var actual = extension.Update();

            Assert.That(actual.Guid, Is.EqualTo(expected.Guid));
            Assert.That(actual.UserGuid, Is.EqualTo(expected.UserGuid));
        }

        [Test]
        public void Delete_CurrentSeesion_ReturnOne()
        {
            var extension = Make_SessionExtension();
            var expected = Make_Session();
            var user = Make_User();
            PortalRepository.Setup(m => m.SessionDelete(expected.Guid, user.Guid)).Returns(1);

            var actual = extension.Delete();

            Assert.That(actual.Value, Is.EqualTo(1));
        }
    }
}