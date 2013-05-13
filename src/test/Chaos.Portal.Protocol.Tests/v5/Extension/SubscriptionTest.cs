namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    using Chaos.Portal.Core.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class SubscriptionTest : TestBase
    {
        [Test, ExpectedException(typeof(InsufficientPermissionsException))]
        public void Create_WithoutPermission_ThrowException()
        {
            var extension = Make_SubscriptionExtension();
            var user      = Make_User();
            user.SystemPermissions = 0;
            PortalRequest.SetupGet(p => p.User).Returns(user);
            
            extension.Create("not allowed");
        }
    }
}