namespace Chaos.Portal.Test.Extension
{
    using System;
    using System.Linq;

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
            CallContext.SetupGet(p => p.Groups).Returns(new[] { expected });

            var result = extension.Get(CallContext.Object).ToList();

            Assert.That(result, Is.Not.Empty);
            Assert.That(result[0], Is.EqualTo(expected));
        }

        [Test]
        public void Create_NewGroup_ReturnNewlyCreatedGroup()
        {
            var extension = Make_GroupExtension();
            var expected  = Make_Group();
            var user      = Make_User();
            CallContext.SetupGet(p => p.User).Returns(user);
            PortalRepository.Setup(m => m.GroupCreate(It.IsAny<Guid>(), expected.Name, user.Guid, (uint)expected.SystemPermission)).Returns(expected);

            var actual = extension.Create(CallContext.Object, expected.Name, (uint)expected.SystemPermission);

            Assert.That(actual, Is.EqualTo(expected));
        }
        
        [Test]
        public void Delete_AsAuthenticatedUser_ReturnOne()
        {
            var extension = Make_GroupExtension();
            var group     = Make_Group();
            var user      = Make_User();
            CallContext.SetupGet(p => p.User).Returns(user);
            PortalRepository.Setup(m => m.GroupDelete(group.Guid, user.Guid)).Returns(1);

            var actual = extension.Delete(CallContext.Object, group.Guid);

            Assert.That(actual.Value, Is.EqualTo(1));
        }
        
        [Test]
        public void Update_AsAuthenticatedUser_ReturnOne()
        {
            var extension = Make_GroupExtension();
            var group     = Make_Group();
            var user      = Make_User();
            CallContext.SetupGet(p => p.User).Returns(user);
            PortalRepository.Setup(m => m.GroupUpdate(group.Guid, user.Guid, group.Name, (uint?)group.SystemPermission)).Returns(1);

            var actual = extension.Update(CallContext.Object, group.Guid, group.Name, (uint?)group.SystemPermission);

            Assert.That(actual.Value, Is.EqualTo(1));
        }
    }
}