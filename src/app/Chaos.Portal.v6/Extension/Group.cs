﻿using Chaos.Portal.Core;

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
			if (userGuid.HasValue)
				throw new NotImplementedException();

			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.GroupGet(guid, null, null);

			return Request.Groups;
		}

		#endregion
		#region Create

		public Core.Data.Model.Group Create(string name, uint systemPermission)
		{
			if (!Request.User.HasPermission(SystemPermissons.CreateGroup)) throw new InsufficientPermissionsException("User does not have permission to create groups");

			return PortalRepository.GroupCreate(Guid.NewGuid(), name, Request.User.Guid, systemPermission);
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

			var result = PortalRepository.GroupUpdate(guid, Request.User.Guid, newName, newSystemPermission);

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