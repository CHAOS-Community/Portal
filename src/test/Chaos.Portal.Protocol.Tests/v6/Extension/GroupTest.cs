namespace Chaos.Portal.Protocol.Tests.v6.Extension
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Core.Exceptions;
	using Core.Data.Model;

	using Moq;

	using NUnit.Framework;

	[TestFixture]
	public class GroupTest : TestBase
	{
		[Test]
		public void Get_HasAdminSystemPermission_ReturnAllGroups()
		{
			var group = Make_GroupExtension();
			var groups = new[] { Make_Group(), Make_Group() };
			PortalRepository.Setup(m => m.GroupGet(null, null, null)).Returns(groups);

			var results = group.Get();

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results, Is.EqualTo(groups));
		}

		[Test]
		public void Get_WithoutAdminSystemPermission_ReturnCurrentUsersGroups()
		{
			var group = Make_GroupExtension();
			var currentUser = Make_User();
			var groups = new[] { Make_Group(), Make_Group() };

			currentUser.SystemPermissonsEnum = SystemPermissons.None;
			
			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRequest.SetupGet(p => p.Groups).Returns(groups);
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			var results = group.Get();

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results.First(), Is.EqualTo(groups.First()));
			PortalRepository.Verify(m => m.GroupGet(null, null, null), Times.Never());
		}

		[Test]
		public void Create_WithPermission_ReturnUser()
		{
			var extension = Make_GroupExtension();
			var expected = Make_Group();
			var user = Make_User();
			user.SystemPermissonsEnum = SystemPermissons.All;
			PortalRequest.SetupGet(p => p.User).Returns(user);
			PortalRepository.Setup(p => p.GroupCreate(It.IsAny<Guid>(), expected.Name, user.Guid, expected.SystemPermission.Value)).Returns(expected);

			var result = extension.Create(expected.Name, (uint)expected.SystemPermission);

			Assert.That(result, Is.EqualTo(expected));
		}

		[Test, ExpectedException(typeof(InsufficientPermissionsException))]
		public void Create_WithoutPermission_ThrowException()
		{
			var extension = Make_GroupExtension();
			var expected = Make_Group();
			var user = Make_User();
			user.SystemPermissonsEnum = SystemPermissons.None;
			PortalRequest.SetupGet(p => p.User).Returns(user);

			extension.Create(expected.Name, (uint)expected.SystemPermission);
		}

		[Test]
		public void Delete_WithAdminSystemPermission_ReturnOne()
		{
			var group = Make_GroupExtension();
			var currentUser = Make_User();
			var groupGuid = Guid.NewGuid();

			currentUser.SystemPermissonsEnum = SystemPermissons.All;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			PortalRepository.Setup(p => p.GroupDelete(groupGuid, currentUser.Guid)).Returns(1);

			var result = group.Delete(groupGuid);

			Assert.That(result.Value, Is.EqualTo(1));
		}

		[Test]
		public void Update_AsAuthenticatedUser_ReturnOne()
		{
			var extension = Make_GroupExtension();
			var group = Make_Group();
			var user = Make_User();
			PortalRepository.Setup(m => m.GroupUpdate(group.Guid, user.Guid, group.Name, (uint?)group.SystemPermission)).Returns(1);

			var actual = extension.Update(group.Guid, group.Name, (uint?)group.SystemPermission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}
	}
}