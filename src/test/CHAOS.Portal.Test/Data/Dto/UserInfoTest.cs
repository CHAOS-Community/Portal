namespace Chaos.Portal.Test.Data.Dto
{
    using Chaos.Portal.Data.Dto;

    using NUnit.Framework;

    [TestFixture]
    public class UserInfoTest
    {
        [Test]
        public void SystemPermissionsEnum_SystemPermissionsIsFour_ShouldHaveManageFlag()
        {
            var userInfo = new UserInfo();
            userInfo.SystemPermissions = 4;

            var actual = userInfo.SystemPermissonsEnum;

            Assert.That(actual.HasFlag(SystemPermissons.Manage), Is.True);
        }

        [Test]
        public void SystemPermissionsEnum_SystemPermissionsIsFive_ShouldHaveManageFlag()
        {
            var userInfo = new UserInfo();
            userInfo.SystemPermissions = 5;

            var actual = userInfo.SystemPermissonsEnum;

            Assert.That( actual.HasFlag( SystemPermissons.Manage ), Is.True );
            Assert.That( actual.HasFlag( SystemPermissons.CreateGroup ), Is.True );
        }
    }
}