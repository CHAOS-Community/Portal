namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    using NUnit.Framework;

    [TestFixture]
    public class UserTest : TestBase
    {
        [Test]
        public void Get_ReturnCurrentUser()
        {
	        var user = Make_UserExtension();

	        var result = user.Get();

            Assert.That(result.Guid, Is.EqualTo(Make_User().Guid));
        }
    }
}
