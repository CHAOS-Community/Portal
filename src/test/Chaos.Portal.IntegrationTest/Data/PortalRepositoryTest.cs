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

        [Test]
        public void GroupUpdate_ChangeName_ReturnOne()
        {
            var group   = Make_GroupThatExist();
            var user    = Make_UserInfoThatExist();
            var newName = "newName";
            
            var actual = PortalRepository.GroupUpdate(group.Guid, user.Guid, newName, null);

            Assert.That(actual, Is.EqualTo(1));
            var results = PortalRepository.GroupGet(group.Guid, newName, user.Guid);
            Assert.That(results, Is.Not.Empty);
        }

        [Test]
        public void SessionGet_GivenGuid_ReturnSession()
        {
            var session = Make_Session();

            var results = PortalRepository.SessionGet(session.Guid, null);
            
            Assert.That(results, Is.Not.Empty);
            var actual = results.First();
            Assert.That(actual.Guid, Is.EqualTo(session.Guid));
            Assert.That(actual.UserGuid, Is.EqualTo(session.UserGuid));
        }

        [Test]
        public void SessionCreate_GivenUserGuid_ReturnNewSession()
        {
            var session = Make_Session();

            var actual = PortalRepository.SessionCreate(session.UserGuid);

            Assert.That(actual.Guid, Is.Not.EqualTo(Guid.Empty));
            Assert.That(actual.UserGuid, Is.EqualTo(session.UserGuid));
        }

        [Test]
        public void SessionUpdate_RenewSession_ReturnSessionWithUpdateDateModified()
        {
            var session = Make_Session();

            var actual = PortalRepository.SessionUpdate(session.Guid, session.UserGuid);

            Assert.That(actual.DateModified, Is.Not.Null);
        }

        [Test]
        public void SessionDelete_Logout_ReturnOne()
        {
            var session = Make_Session();

            var actual = PortalRepository.SessionDelete(session.Guid, session.UserGuid);

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void ClientSettingsGet_GivenGuid_ReturnClientSettings()
        {
            var clientSettings = Make_ClientSettingsThatExist();

            var actual = PortalRepository.ClientSettingsGet(clientSettings.Guid);

            Assert.That(actual.Guid, Is.EqualTo(clientSettings.Guid));
            Assert.That(actual.Name, Is.EqualTo(clientSettings.Name));
            Assert.That(actual.DateCreated, Is.EqualTo(clientSettings.DateCreated));
            Assert.That(actual.Settings, Is.EqualTo(clientSettings.Settings));
        }

        [Test]
        public void ClientSettingsSet_CreateNewClientSettings_ReturnOne()
        {
            var clientSettings = Make_ClientSettingsThatExist();
            var newGuid        = new Guid("00000000-0000-0000-0000-000000002000");

            var actual = PortalRepository.ClientSettingsSet(newGuid, clientSettings.Name, clientSettings.Settings);

            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void SubscriptionGet_GivenUserGuid_ReturnAllOfUsersSubscriptions()
        {
            var subscription = Make_Subscription();

            var results = PortalRepository.SubscriptionGet(null, subscription.UserGuid);

            Assert.That(results, Is.Not.Empty);
            var actual = results.First();
            Assert.That(actual.Guid, Is.EqualTo(subscription.Guid));
            Assert.That(actual.UserGuid, Is.EqualTo(subscription.UserGuid));
            Assert.That(actual.Name, Is.EqualTo(subscription.Name));
            Assert.That(actual.Permission, Is.EqualTo(subscription.Permission));
            Assert.That(actual.DateCreated, Is.EqualTo(subscription.DateCreated));
        }

        [Test]
        public void SubscriptionCreate_WithPermission_ReturnNewSubscription()
        {
            var subscription = Make_Subscription();
            var newGuid      = new Guid("00000000-0000-0000-0000-000000020000");

            var actual = PortalRepository.SubscriptionCreate(newGuid, subscription.Name, subscription.UserGuid);

            Assert.That(actual.Guid, Is.EqualTo(newGuid));
            Assert.That(actual.UserGuid, Is.EqualTo(subscription.UserGuid));
            Assert.That(actual.Name, Is.EqualTo(subscription.Name));
            Assert.That(actual.Permission, Is.EqualTo(subscription.Permission));
        }

        [Test]
        public void SubscriptionDelete_WithPermission_ReturnOne()
        {
            var subscription = Make_Subscription();

            var actual = PortalRepository.SubscriptionDelete(subscription.Guid, subscription.UserGuid);

            Assert.That(actual, Is.EqualTo(1));
        }

        private SubscriptionInfo Make_Subscription()
        {
            return new SubscriptionInfo
                {
                    Guid        = new Guid("00000000-0000-0000-0000-000000010000"),
                    Name        = "test subscription",
                    UserGuid    = Make_UserInfoThatExist().Guid,
                    Permission  = SubscriptionPermission.Max,
                    DateCreated = new DateTime(2000,01,01,00,00,00)
                };
        }

        private ClientSettings Make_ClientSettingsThatExist()
        {
            return new ClientSettings
                {
                    Guid        = new Guid("00000000-0000-0000-0000-000000001000"),
                    Name        = "test client",
                    Settings    = "test settings",
                    DateCreated = new DateTime(2000,01,01,00,00,00)
                };
        }

        private Session Make_Session()
        {
            return new Session
                {
                    Guid         = new Guid("00000000-0000-0000-0000-000000000100"),
                    UserGuid     = Make_UserInfoThatExist().Guid
                };
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