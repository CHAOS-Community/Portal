namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Exceptions;

    [PortalExtension(configurationName: "Portal")]
    public class Group : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get
        
        public IEnumerable<Data.Dto.Group> Get( ICallContext callContext)
        {
            return callContext.Groups;
        }

        #endregion
        #region Create

        public Data.Dto.Group Create( ICallContext callContext, string name, uint systemPermission )
        {
            if( callContext.IsAnonymousUser ) throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            return PortalRepository.GroupCreate(new Guid(), name, callContext.User.Guid, systemPermission);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, Guid guid )
        {
            if(callContext.IsAnonymousUser) throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            var result = PortalRepository.GroupDelete(guid, callContext.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Update

        public ScalarResult Update( ICallContext callContext, Guid guid, string newName, uint? newSystemPermission )
        {
            if(callContext.IsAnonymousUser) throw new InsufficientPermissionsException( "Anonymous users cannot Update groups" );

            var result = PortalRepository.GroupUpdate(guid, callContext.User.Guid, newName, newSystemPermission);

            return new ScalarResult((int) result);
        }

        #endregion
    }
}
