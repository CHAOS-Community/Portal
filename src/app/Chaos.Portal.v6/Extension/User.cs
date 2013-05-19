using System;
using Chaos.Portal.Core.Exceptions;

namespace Chaos.Portal.v6.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Data.Model;
    using Core.Extension;

    using Core;

    public class User : AExtension
    {
        #region Initialization

        public User(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
	
		#region Create

		public UserInfo Create(Guid? guid, string email)
		{
			if (!Request.User.HasPermission(SystemPermissons.UserManager)) throw new InsufficientPermissionsException();

			if (!guid.HasValue)
				guid = Guid.NewGuid();

			if(PortalRepository.UserCreate(guid.Value, email) <= 0)
				throw new Exception("Failed to create user");
			
			return PortalRepository.UserInfoGet(guid, null, null, null).First();
		}

		#endregion
		#region Update

		public UserInfo Update(Guid guid, string email, uint? permissons)
		{
            if(!Request.User.HasPermission(SystemPermissons.UserManager) && (guid != Request.User.Guid || permissons.HasValue)) throw new InsufficientPermissionsException();
            if(PortalRepository.UserUpdate(guid, email, permissons) <= 0)throw new Exception("Failed to update user");

			return PortalRepository.UserInfoGet(guid, null, null, null).First();
		}

		#endregion
		#region Delete

		public ScalarResult Delete(Guid guid)
		{
            if (!Request.User.HasPermission(SystemPermissons.UserManager)) throw new InsufficientPermissionsException();

			var result = new ScalarResult((int) PortalRepository.UserDelete(guid));

			if(result.Value <= 0)
				throw new Exception("Failed to delete user");

			return result;
		}

		#endregion
		#region Get

		public IEnumerable<UserInfo> Get(Guid? guid = null, Guid? groupGuid = null)
        {
			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.UserInfoGet(guid, null, null, groupGuid);

			if(guid.HasValue)
				throw new NotImplementedException("Specifing guid without UserManager rights not implemented");

			if (groupGuid.HasValue)
				throw new NotImplementedException("Specifing groupGuid without UserManager rights not implemented");

            var result = PortalRepository.UserInfoGetWithGroupPermission(Request.User.Guid);

            return result.Any() ? result : new[] { Request.User };
        }

		public UserInfo GetCurrent()
		{
            return Request.User;
		}

        #endregion
	}
}