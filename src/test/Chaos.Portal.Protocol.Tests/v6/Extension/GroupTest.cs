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
			var group = Make_v6GroupExtension();
			var groups = new[] { Make_Group(), Make_Group() };
			PortalRepository.Setup(m => m.GroupGet(null, null, null)).Returns(groups);

			var results = group.Get();

			Assert.That(results.Count(), Is.EqualTo(2));
			Assert.That(results, Is.EqualTo(groups));
		}

		[Test]
		public void Get_WithoutAdminSystemPermission_ReturnCurrentUsersGroups()
		{
			var group = Make_v6GroupExtension();
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
		public void Delete_WithAdminSystemPermission_DeleteGroup()
		{
			var group = Make_v6GroupExtension();
			var currentUser = Make_User();
			var groupGuid = Guid.NewGuid();

			currentUser.SystemPermissonsEnum = SystemPermissons.All;

			PortalRequest.SetupGet(p => p.User).Returns(currentUser);
			PortalRequest.SetupGet(m => m.Parameters).Returns(new Dictionary<string, string>() { { "sessionGUID", Make_Session().Guid.ToString() } });

			PortalRepository.Setup(p => p.GroupDelete(groupGuid, currentUser.Guid)).Returns(1);

			var result = group.Delete(groupGuid);

			Assert.That(result.Value, Is.EqualTo(1));
		}
	}
}