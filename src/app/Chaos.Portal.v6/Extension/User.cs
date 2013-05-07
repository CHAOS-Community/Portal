using System;
using Chaos.Portal.Core.Exceptions;

namespace Chaos.Portal.v6.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class User : AExtension
	{
		#region Create

		public UserInfo Create(Guid? guid, string email)
		{
			if (!User.HasPermission(SystemPermissons.UserManager))
				throw new InsufficientPermissionsException();

			if (!guid.HasValue)
				guid = Guid.NewGuid();

			if(PortalRepository.UserCreate(guid.Value, email) <= 0)
				throw new Exception("Failed to create user");
			
			return PortalRepository.UserInfoGet(guid, null, null).First();
		}

		#endregion
		#region Update

		public UserInfo Update(Guid guid, string email, SystemPermissons? permissons)
		{
			throw new NotImplementedException();
		}

		#endregion
		#region Delete

		public ScalarResult Delete(Guid guid)
		{
			throw new NotImplementedException();
		}

		#endregion
		#region Get

		public IEnumerable<UserInfo> Get()
        {
			if (User.HasPermission(SystemPermissons.UserManager))
				return PortalRepository.UserInfoGet(null, null, null);

	        var result = PortalRepository.UserInfoGetWithGroupPermission(User.Guid);

	        return result.Any() ? result : new[] {User};
        }

		public UserInfo GetCurrent()
		{
			return User;
		}

        #endregion
    }
}