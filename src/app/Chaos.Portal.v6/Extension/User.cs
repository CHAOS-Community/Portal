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
			
			return PortalRepository.UserInfoGet(guid, null, null).First();
		}

		#endregion
		#region Update

		public UserInfo Update(Guid guid, string email, uint? permissons)
		{
            if(!Request.User.HasPermission(SystemPermissons.UserManager) && (guid != Request.User.Guid || permissons.HasValue)) throw new InsufficientPermissionsException();
            if(PortalRepository.UserUpdate(guid, email, permissons) <= 0)throw new Exception("Failed to update user");

			return PortalRepository.UserInfoGet(guid, null, null).First();
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

		public IEnumerable<UserInfo> Get(bool includeGroups = false)
        {
			if(includeGroups)
				throw new NotImplementedException();

			if (Request.User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.UserInfoGet(null, null, null);

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