namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Exceptions;

    [PortalExtension(configurationName: "Portal")]
    public class Group : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get
        
        public IEnumerable<IGroup> Get( ICallContext callContext, Guid guid )
        {
            return callContext.Groups;
        }

        #endregion
        #region Create

        public IGroup Create( ICallContext callContext, string name, uint systemPermission )
        {
            if( callContext.User.Guid.ToString() == callContext.AnonymousUserGuid.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            return PortalRepository.GroupCreate(new Guid(), name, callContext.User.Guid, systemPermission);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, Guid guid )
        {
            if(callContext.User.Guid.ToString() == callContext.AnonymousUserGuid.ToString())
                throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            var result = PortalRepository.GroupDelete(guid, callContext.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Update

        public ScalarResult Update( ICallContext callContext, Guid guid, string newName, uint? newSystemPermission )
        {
            if(callContext.User.Guid.ToString() == callContext.AnonymousUserGuid.ToString())
                throw new InsufficientPermissionsException( "Anonymous users cannot Update groups" );

            var result = PortalRepository.GroupUpdate(guid, callContext.User.Guid, newName, newSystemPermission);

            return new ScalarResult((int) result);
        }

        #endregion
    }
}
