namespace Chaos.Portal.v6.Extension
{
	using System;
	using System.Collections.Generic;

	using Core.Data.Model;
	using Core.Exceptions;
	using Core.Extension;

	public class Group : AExtension
	{
		#region Get

		public IEnumerable<Core.Data.Model.Group> Get(Guid? guid = null, bool includeUsers = false)
		{
			if (includeUsers)
				throw new NotImplementedException();

			if (User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.GroupGet(guid, null, null);

			return Groups;
		}

		#endregion
		#region Create

		public Core.Data.Model.Group Create(string name, uint systemPermission)
		{
			if (!User.HasPermission(SystemPermissons.CreateGroup)) throw new InsufficientPermissionsException("User does not have permission to create groups");

			return PortalRepository.GroupCreate(Guid.NewGuid(), name, User.Guid, systemPermission);
		}

		#endregion
		#region Delete

		public ScalarResult Delete(Guid guid)
		{
			if (IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot delete groups");

			var result = PortalRepository.GroupDelete(guid, User.Guid);

			return new ScalarResult((int)result);
		}

		#endregion
		#region Update

		public ScalarResult Update(Guid guid, string newName, uint? newSystemPermission)
		{
			if (IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot Update groups");

			var result = PortalRepository.GroupUpdate(guid, User.Guid, newName, newSystemPermission);

			return new ScalarResult((int)result);
		}

		#endregion
		#region Add / Remove User

		public ScalarResult AddUser(Guid guid, Guid userGuid)
		{
			throw new NotImplementedException();

		}

		public ScalarResult RemoveUser(Guid guid, Guid userGuid)
		{
			throw new NotImplementedException();

		}

		#endregion
	}
}
