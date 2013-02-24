namespace Chaos.Portal.Test.Extension
{
    using NUnit.Framework;

    [TestFixture]
    public class SessionTest : TestBase
    {
        [Test]
        public void Get_CurrentSession_ReturnCurrentSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();
            CallContext.SetupGet(p => p.Session).Returns(expected);

            var actual = extension.Get(CallContext.Object);

            Assert.That(actual, Is.SameAs(expected));
        }
        
        [Test]
        public void Create_NewSession_ReturnSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();
            CallContext.SetupGet(p => p.AnonymousUserGuid).Returns(expected.Guid);
            PortalRepository.Setup(m => m.SessionCreate(expected.Guid)).Returns(expected);

            var actual = extension.Create(CallContext.Object);

            Assert.That(actual, Is.SameAs(expected));
        }
        
        [Test]
        public void Update_RenewCurrentSeesion_ReturnSession()
        {
            var extension = Make_SessionExtension();
            var expected  = Make_Session();
            var user      = Make_User();
            CallContext.SetupGet(p => p.User).Returns(user);
            CallContext.SetupGet(p => p.Session).Returns(expected);
            PortalRepository.Setup(m => m.SessionUpdate(expected.Guid, user.Guid)).Returns(expected);

            var actual = extension.Update(CallContext.Object);

            Assert.That(actual, Is.SameAs(expected));
        }

        [Test]
        public void Delete_CurrentSeesion_ReturnOne()
        {
            var extension = Make_SessionExtension();
            var expected = Make_Session();
            var user = Make_User();
            CallContext.SetupGet(p => p.User).Returns(user);
            CallContext.SetupGet(p => p.Session).Returns(expected);
            PortalRepository.Setup(m => m.SessionDelete(expected.Guid, user.Guid)).Returns(1);

            var actual = extension.Delete(CallContext.Object);

            Assert.That(actual.Value, Is.EqualTo(1));
        }
    }
}