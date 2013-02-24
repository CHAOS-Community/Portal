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
    }
}