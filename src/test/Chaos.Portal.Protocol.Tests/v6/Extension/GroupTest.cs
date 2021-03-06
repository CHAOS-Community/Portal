﻿namespace Chaos.Portal.Protocol.Tests.v6.Extension
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
			PortalRepository.Setup(m => m.GroupGet(null, null, null, null)).Returns(groups);

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
			PortalRepository.Verify(m => m.GroupGet(null, null, null, null), Times.Never());
		}

		[Test]
		public void Get_HasAdminSystemPermissionAndUserGuid_ReturnUsersGroups()
		{
			var group = Make_GroupExtension();
			var groups = new[] { Make_Group(), Make_Group() };
			var userGuid = Guid.NewGuid();

			PortalRepository.Setup(m => m.GroupGet(null, null, null, userGuid)).Returns(groups);

			var results = group.Get(null, userGuid);

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results, Is.EqualTo(groups));
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
			PortalRepository.Setup(m => m.GroupUpdate(group.Guid, user.Guid, group.Name, group.SystemPermission)).Returns(1);

			var actual = extension.Update(group.Guid, group.Name, group.SystemPermission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void AddUser_WithAdminSystemPermissio_ReturnOne()
		{
			var extension = Make_GroupExtension();
			var currentUser = Make_User();
			var group = Make_Group();
			var user = Make_User();
			var permission = 0u;

			currentUser.SystemPermissonsEnum = SystemPermissons.All;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(m => m.GroupAddUser(group.Guid, user.Guid, permission, null)).Returns(1);

			var actual = extension.AddUser(group.Guid, user.Guid, permission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void AddUser_WithoutAdminSystemPermissio_ReturnOne()
		{
			var extension = Make_GroupExtension();
			var currentUser = Make_User();
			var group = Make_Group();
			var user = Make_User();
			var permission = 0u;

			currentUser.SystemPermissonsEnum = SystemPermissons.None;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(m => m.GroupAddUser(group.Guid, user.Guid, permission, currentUser.Guid)).Returns(1);

			var actual = extension.AddUser(group.Guid, user.Guid, permission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void RemoveUser_WithAdminSystemPermissio_ReturnOne()
		{
			var extension = Make_GroupExtension();
			var currentUser = Make_User();
			var group = Make_Group();
			var user = Make_User();

			currentUser.SystemPermissonsEnum = SystemPermissons.All;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(m => m.GroupRemoveUser(group.Guid, user.Guid, null)).Returns(1);

			var actual = extension.RemoveUser(group.Guid, user.Guid);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void RemoveUser_WithoutAdminSystemPermissio_ReturnOne()
		{
			var extension = Make_GroupExtension();
			var currentUser = Make_User();
			var group = Make_Group();
			var user = Make_User();

			currentUser.SystemPermissonsEnum = SystemPermissons.None;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(m => m.GroupRemoveUser(group.Guid, user.Guid, currentUser.Guid)).Returns(1);

			var actual = extension.RemoveUser(group.Guid, user.Guid);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void UpdateUserPermissions_WithUserManagerPermission_UpdateUser()
		{
			var currentUser = Make_User();
			var user = Make_User();
			var group = Make_Group();
			var newPermission = 32u;

			currentUser.SystemPermissonsEnum = SystemPermissons.UserManager;

			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });
			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(p => p.GroupUpdateUserPermissions(group.Guid, user.Guid, newPermission, null)).Returns(1);

			var actual = Make_GroupExtension().UpdateUserPermissions(group.Guid, user.Guid, newPermission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}

		[Test]
		public void UpdateUserPermissions_WithoutUserManagerPermission_UpdateUser()
		{
			var currentUser = Make_User();
			var user = Make_User();
			var group = Make_Group();
			var newPermission = 32u;

			currentUser.SystemPermissonsEnum = SystemPermissons.None;

			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });
			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRepository.Setup(p => p.GroupUpdateUserPermissions(group.Guid, user.Guid, newPermission, currentUser.Guid)).Returns(1);

			var actual = Make_GroupExtension().UpdateUserPermissions(group.Guid, user.Guid, newPermission);

			Assert.That(actual.Value, Is.EqualTo(1));
		}
	}
}