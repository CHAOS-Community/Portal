using Chaos.Portal.Core;

namespace Chaos.Portal.v6.Extension
{
	using System;
	using System.Collections.Generic;

	using Core.Data.Model;
	using Core.Exceptions;
	using Core.Extension;

	public class Group : AExtension
	{
		#region Initialization

		public Group(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
		#region Get

		public IEnumerable<Core.Data.Model.Group> Get(Guid? guid = null, Guid? userGuid = null)
		{
			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.GroupGet(guid, null, null, userGuid);

			if (userGuid.HasValue)
				throw new NotImplementedException("Specifying UserGuid without UserManager permission not implemented");

			return Request.Groups;
		}

		#endregion
		#region Create

		public Core.Data.Model.Group Create(string name, uint systemPermission)
		{
			if (!Request.User.HasPermission(SystemPermissons.CreateGroup)) throw new InsufficientPermissionsException("User does not have permission to create groups");

			return PortalRepository.GroupCreate(Guid.NewGuid(), name, Request.User.Guid, systemPermission); //TODO: Handle what permissions can be given
		}

		#endregion
		#region Delete

		public ScalarResult Delete(Guid guid)
		{
			if (Request.IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot delete groups");

			var result = PortalRepository.GroupDelete(guid, Request.User.Guid);

			return new ScalarResult((int)result);
		}

		#endregion
		#region Update

		public ScalarResult Update(Guid guid, string newName, uint? newSystemPermission)
		{
			if (Request.IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot Update groups");

			var result = PortalRepository.GroupUpdate(guid, Request.User.Guid, newName, newSystemPermission);//TODO: Handle what permissions can be given

			return new ScalarResult((int)result);
		}

		#endregion
		#region Add / Remove User

		public ScalarResult AddUser(Guid guid, Guid userGuid, uint permissions)
		{
			if (Request.IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot add users to groups");

			if(Request.User.HasPermission(SystemPermissons.UserManager))
				return new ScalarResult((int) PortalRepository.GroupAddUser(guid, userGuid, permissions, null)); //TODO: Handle what permissions can be given

			return new ScalarResult((int)PortalRepository.GroupAddUser(guid, userGuid, permissions, Request.User.Guid)); //TODO: Handle what permissions can be given
		}

		public ScalarResult RemoveUser(Guid guid, Guid userGuid)
		{
			if (Request.IsAnonymousUser) throw new InsufficientPermissionsException("Anonymous users cannot remover users from groups");

			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return new ScalarResult((int)PortalRepository.GroupRemoveUser(guid, userGuid, null));

			return new ScalarResult((int)PortalRepository.GroupRemoveUser(guid, userGuid, Request.User.Guid)); //TODO: Handle what permissions can be given
		}

		#endregion
		#region Update User Permissions

		public ScalarResult UpdateUserPermissions(Guid guid, Guid userGuid, uint permissions)
		{
			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return new ScalarResult((int) PortalRepository.GroupUpdateUserPermissions(guid, userGuid, permissions, null));

			return new ScalarResult((int) PortalRepository.GroupUpdateUserPermissions(guid, userGuid, permissions, Request.User.Guid)); //TODO: Handle what permissions can be given
		}

		#endregion
	}
}
