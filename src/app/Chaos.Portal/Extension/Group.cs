namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;

    [PortalExtension(configurationName: "Portal")]
    public class Group : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get
        
        public IEnumerable<Core.Data.Model.Group> Get()
        {
            return Groups;
        }

        #endregion
        #region Create

        public Core.Data.Model.Group Create(string name, uint systemPermission )
        {
            if(!User.HasPermission(SystemPermissons.CreateGroup) ) throw new InsufficientPermissionsException( "User does not have permission to create groups" );

            return PortalRepository.GroupCreate(new Guid(), name, User.Guid, systemPermission);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(Guid guid )
        {
            if(IsAnonymousUser) throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            var result = PortalRepository.GroupDelete(guid, User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Update

        public ScalarResult Update(Guid guid, string newName, uint? newSystemPermission )
        {
            if(IsAnonymousUser) throw new InsufficientPermissionsException( "Anonymous users cannot Update groups" );

            var result = PortalRepository.GroupUpdate(guid, User.Guid, newName, newSystemPermission);

            return new ScalarResult((int) result);
        }

        #endregion
    }
}
