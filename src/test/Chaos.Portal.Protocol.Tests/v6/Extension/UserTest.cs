using Chaos.Portal.Core.Exceptions;

namespace Chaos.Portal.Protocol.Tests.v6.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Data.Model;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
	public class UserTest : TestBase
	{
		[Test]
		public void GetCurrent_ReturnCurrentUser()
		{
			var user = Make_UserExtension();

			var result = user.GetCurrent();

            Assert.That(result.Guid, Is.EqualTo(Make_User().Guid));
		}

		[Test]
		public void Get_HasAdminSystemPermission_ReturnAllUsers()
		{
			var user  = Make_UserExtension();
			var users = new[] { Make_User(), Make_User() };
			PortalRepository.Setup(m => m.UserInfoGet(null, null, null)).Returns(users);

			var results = user.Get();

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results, Is.EqualTo(users));
		}

		[Test]
		public void Get_WithoutAdminSystemPermission_ReturnCurrentUser()
		{
			var user        = Make_UserExtension();
			var currentUser = Make_User();
			currentUser.SystemPermissonsEnum = SystemPermissons.None;
			PortalRepository.Setup(m => m.UserInfoGet(null, It.Is<Guid?>(item => item.HasValue), null)).Returns(new[] { currentUser });
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			var results = user.Get();

			Assert.That(results.Count(), Is.EqualTo(1));
			Assert.That(results.First(), Is.EqualTo(currentUser));
			PortalRepository.Verify(m => m.UserInfoGet(null, null, null), Times.Never());
		}

		[Test]
		public void Get_WithPermissionViaGroup_ReturnUsersFromGroups()
		{
			var user = Make_UserExtension();
			var users = new[] { Make_User(), Make_User() };
			var currentUser = Make_User();
			currentUser.SystemPermissonsEnum = SystemPermissons.None;
			PortalRepository.Setup(m => m.UserInfoGetWithGroupPermission(currentUser.Guid)).Returns(users);
			PortalRepository.Setup(m => m.UserInfoGet(null, It.Is<Guid?>(item => item.HasValue), null)).Returns(new[] { currentUser });
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			var results = user.Get();

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results, Is.EqualTo(users));
		}

		[Test]
		public void Create_WithAdminSystemPermission_ReturnNewUser()
		{
			var user = Make_UserExtension();
			var currentUser = Make_User();
			var newUser = Make_User();

			PortalRepository.Setup(p => p.UserCreate(It.IsAny<Guid>(), newUser.Email)).Returns(1);

			PortalRepository.Setup(m => m.UserInfoGet(null, It.Is<Guid?>(item => item.HasValue), null)).Returns(new[] { currentUser }); //Return the current user for permission check
			PortalRepository.Setup(p => p.UserInfoGet(It.Is<Guid?>(i => i.HasValue), null, null)).Returns(new[] {newUser}); //Return the created user
			
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			var result = user.Create(null, newUser.Email);

			Assert.That(result, Is.Not.Null);
			Assert.That(result, Is.EqualTo(newUser));
		}

		[Test, ExpectedException(typeof(InsufficientPermissionsException))]
		public void Create_WithoutAdminSystemPermission_ThrowInsufficientPermissionsException()
		{
			var user = Make_UserExtension();
			var currentUser = Make_User();
			currentUser.SystemPermissonsEnum = SystemPermissons.None;

			PortalRepository.Setup(m => m.UserInfoGet(null, It.Is<Guid?>(item => item.HasValue), null)).Returns(new[] { currentUser }); //Return the current user for permission check

			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			user.Create(null, "name@domain.com");
		}
	}
}