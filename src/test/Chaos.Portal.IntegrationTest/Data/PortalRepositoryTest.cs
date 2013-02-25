namespace Chaos.Portal.IntegrationTest.Data
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Chaos.Deployment.UI.Console.Action.Database.Import;
    using Chaos.Portal.Data;
    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class PortalRepositoryTest
    {
        protected IPortalRepository PortalRepository { get; set; }

        [SetUp]
        public void SetUp()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["portal"].ConnectionString;
            var importer         = new ImportDeployment();

            importer.Parameters.ConnectionString = connectionString;
            importer.Parameters.Path = @"..\..\..\..\..\sql\6.data\initial.sql";

            importer.Run();

            importer.Parameters.Path = "integraion_tests_base_data.sql";

            importer.Run();

            PortalRepository = new PortalRepository().WithConfiguration(connectionString);
        }

        [Test]
        public void UserInfoGet_ByGuidUserInfoExists_ReturnUserInfo()
        {
            var userThatExist = Make_UserInfoThatExist();

            var results = PortalRepository.GetUserInfo(userThatExist.Guid, null, null);

            Assert.That(results, Is.Not.Empty);
            var actual = results.First();
            Assert.That(actual.Guid, Is.EqualTo(userThatExist.Guid));
            Assert.That(actual.Email, Is.EqualTo(userThatExist.Email));
        }

        [Test]
        public void UserInfoGet_ByEmailGiveUserExist_ReturnUserInfo()
        {
            var userThatExist = Make_UserInfoThatExist();

            var actual = PortalRepository.GetUserInfo(userThatExist.Email);

            Assert.That(actual.Guid, Is.EqualTo(userThatExist.Guid));
            Assert.That(actual.Email, Is.EqualTo(userThatExist.Email));
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void UserInfoGet_ByEmailGiveUserTheDoesntExist_ThrowArgumentException()
        {
            PortalRepository.GetUserInfo("fake@fake.fake");
        }

        [Test]
        public void GroupGet_GivenGuidOfGroupThatExist_ReturnGroup()
        {
            var groupThatExist = Make_GroupThatExist();

            var results = PortalRepository.GroupGet(groupThatExist.Guid, null, null);

            Assert.That(results, Is.Not.Empty);
            var actual = results.First();
            Assert.That(actual.Guid, Is.EqualTo(groupThatExist.Guid));
            Assert.That(actual.Name, Is.EqualTo(groupThatExist.Name));
            Assert.That(actual.SystemPermission, Is.EqualTo(groupThatExist.SystemPermission));
            Assert.That(actual.DateCreated, Is.EqualTo(groupThatExist.DateCreated));
        }

        [Test]
        public void GroupCreate_WithPermission_ReturnCreatedGroup()
        {
            var group = Make_GroupThatDoesntExist();
            var user  = Make_UserInfoThatExist();

            var actual = PortalRepository.GroupCreate(group.Guid, group.Name, user.Guid, user.SystemPermissions.Value);

            Assert.That(actual.Guid, Is.EqualTo(group.Guid));
            Assert.That(actual.Name, Is.EqualTo(group.Name));
            Assert.That(actual.SystemPermission, Is.EqualTo(group.SystemPermission));
        }

        [Test, ExpectedException(typeof(InsufficientPermissionsException))]
        public void GroupCreate_GivenSystemPermissionHigherThatWhatTheUserHas_ThrowInssuficientPermissionException()
        {
            var group = Make_GroupThatDoesntExist();
            var user  = Make_UserInfoThatExist();

            PortalRepository.GroupCreate(group.Guid, group.Name, user.Guid, uint.MaxValue);
        }

        [Test]
        public void GroupDelete_WithPermission_ReturnOne()
        {
            var group = Make_GroupThatExist();
            var user  = Make_UserInfoThatExist();

            var actual = PortalRepository.GroupDelete(group.Guid, user.Guid);

            Assert.That(actual, Is.EqualTo(1));
        }

        private Group Make_GroupThatExist()
        {
            return new Group
                {
                    Guid             = new Guid("00000000-0000-0000-0000-000000000010"),
                    Name             = "test group",
                    DateCreated      = new DateTime(2000,01,01,00,00,00),
                    SystemPermission = 255
                };
        }

        private Group Make_GroupThatDoesntExist()
        {
            return new Group
                {
                    Guid             = new Guid("00000000-0000-0000-0000-000000000020"),
                    Name             = "test group doesnt exist",
                    DateCreated      = new DateTime(2000,01,01,00,00,00),
                    SystemPermission = 255
                };
        }

        private UserInfo Make_UserInfoThatExist()
        {
            return new UserInfo
                {
                    Guid              = new Guid("00000000-0000-0000-0000-000000000001"),
                    Email             = "test@test.test",
                    SystemPermissions = 255
                };
        }
    }
}