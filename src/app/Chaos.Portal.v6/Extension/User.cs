namespace Chaos.Portal.v6.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Data.Model;
    using Core.Exceptions;
    using Core.Extension;

    using Core;

    public class User : AExtension
    {
        public User(IPortalApplication portalApplication): base(portalApplication)
        {
        }
	
		public UserInfo Create(Guid? guid, string email)
		{
			ThrowIfUserDoesntHavePermission();

			if (!guid.HasValue)
				guid = Guid.NewGuid();

			if(PortalRepository.UserCreate(guid.Value, email) <= 0)
				throw new Exception("Failed to create user");
			
			return PortalRepository.UserInfoGet(guid, null, null, null).First();
		}

        // todo: User/Update not implemented
		public UserInfo Update(Guid guid, string email, uint? permissons)
		{
            if(!HasUserManagerPermission() && (guid != Request.User.Guid || permissons.HasValue)) throw new InsufficientPermissionsException();

            throw new NotImplementedException();
		}

        // todo: User/Delete not implemented
		public ScalarResult Delete(Guid guid)
		{
            ThrowIfUserDoesntHavePermission();

            throw new NotImplementedException();
		}

		public IEnumerable<UserInfo> Get(Guid? guid = null, Guid? groupGuid = null)
        {
			if (HasUserManagerPermission())
				return PortalRepository.UserInfoGet(guid, null, null, groupGuid);

			if(guid.HasValue)
				throw new NotImplementedException("Specifing guid without UserManager rights not implemented");

			if (groupGuid.HasValue)
				throw new NotImplementedException("Specifing groupGuid without UserManager rights not implemented");

            var result = PortalRepository.UserInfoGetWithGroupPermission(Request.User.Guid);

            return result.Any() ? result : new[] { Request.User };
        }

		public IEnumerable<UserInfo> GetCurrent()
		{
			return Get(Request.User.Guid);
		}

        private void ThrowIfUserDoesntHavePermission()
        {
            if (!HasUserManagerPermission()) throw new InsufficientPermissionsException();
        }

        private bool HasUserManagerPermission()
        {
            return Request.User.HasPermission(SystemPermissons.UserManager);
        }
    }
}