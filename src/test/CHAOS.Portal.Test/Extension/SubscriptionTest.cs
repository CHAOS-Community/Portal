namespace Chaos.Portal.Test.Extension
{
    using System;

    using Chaos.Portal.Core.Exceptions;

    using Moq;

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
            PortalRepository.Setup(m => m.UserInfoGet(null, It.IsAny<Guid?>(), null)).Returns(new[] { user });
            
            extension.Create("not allowed");
        }
    }
}