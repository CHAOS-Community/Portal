namespace Chaos.Portal.Protocol.Tests.v5.Extension
{
    using System;
    using System.Linq;

    using Chaos.Portal.Core.Exceptions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class GroupTest : TestBase
    {
        [Test]
        public void Get_GroupsTheCurrentUserIsIn_ReturnListOfGroups()
        {
            var extension = Make_GroupExtension();
            var expected  = Make_Group();
            PortalRequest.SetupGet(p => p.Groups).Returns(new[] { expected });

            var result = extension.Get().ToList();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result[0].Guid, Is.EqualTo(expected.Guid));
        }

        [Test]
        public void Create_NewGroup_ReturnNewlyCreatedGroup()
        {
            var extension = Make_GroupExtension();
            var expected  = Make_Group();
            var user      = Make_User();
            PortalRepository.Setup(m => m.GroupCreate(It.IsAny<Guid>(), expected.Name, user.Guid, (uint)expected.SystemPermission)).Returns(expected);

            var actual = extension.Create(expected.Name, (uint)expected.SystemPermission);

            Assert.That(actual.Guid, Is.EqualTo(expected.Guid));
        }
        
        [Test, ExpectedException(typeof(InsufficientPermissionsException))]
        public void Create_WithoutPermission_ThrowException()
        {
            var extension = Make_GroupExtension();
            var expected  = Make_Group();
            var user      = Make_User();
            user.SystemPermissions = 0;
            PortalRequest.SetupGet(p => p.User).Returns(user);

            extension.Create(expected.Name, (uint)expected.SystemPermission);
        }
        
        [Test]
        public void Delete_AsAuthenticatedUser_ReturnOne()
        {
            var extension = Make_GroupExtension();
            var group     = Make_Group();
            var user      = Make_User();
            PortalRepository.Setup(m => m.GroupDelete(group.Guid, user.Guid)).Returns(1);

            var actual = extension.Delete(group.Guid);

            Assert.That(actual.Value, Is.EqualTo(1));
        }
        
        [Test]
        public void Update_AsAuthenticatedUser_ReturnOne()
        {
            var extension = Make_GroupExtension();
            var group     = Make_Group();
            var user      = Make_User();
            PortalRepository.Setup(m => m.GroupUpdate(group.Guid, user.Guid, group.Name, (uint?)group.SystemPermission)).Returns(1);

            var actual = extension.Update(group.Guid, group.Name, (uint?)group.SystemPermission);

            Assert.That(actual.Value, Is.EqualTo(1));
        }
    }
}