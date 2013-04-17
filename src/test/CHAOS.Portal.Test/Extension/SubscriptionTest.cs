namespace Chaos.Portal.Test.Extension
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
            var user = Make_User();
            user.SystemPermissions = 0;
            CallContext.SetupGet(p => p.User).Returns(user);
            
            extension.Create(CallContext.Object, "not allowed");
        }
    }
}