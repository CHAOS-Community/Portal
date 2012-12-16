using System;
using System.Collections.Generic;
using CHAOS.Extensions;
using CHAOS.Portal.Exception;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Extension.Standard
{
    [PortalExtension( configurationName : "Portal")]
    public class Group : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get
        
        public IEnumerable<IGroup> Get( ICallContext callContext, Guid guid )
        {
            if(callContext.User.GUID.ToString() == callContext.AnonymousUserGUID.ToString())
                throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            return PortalRepository.GroupGet(guid, null, callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Create

        public IGroup Create( ICallContext callContext, string name, uint systemPermission )
        {
            if( callContext.User.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            return PortalRepository.GroupCreate(new Guid(), name, callContext.User.GUID.ToGuid(), systemPermission);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, Guid guid )
        {
            if(callContext.User.GUID.ToString() == callContext.AnonymousUserGUID.ToString())
                throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            var result = PortalRepository.GroupDelete(guid, callContext.User.GUID.ToGuid());

            return new ScalarResult((int) result);
        }

        #endregion
        #region Update

        public ScalarResult Update( ICallContext callContext, Guid guid, string newName, uint? newSystemPermission )
        {
            if(callContext.User.GUID.ToString() == callContext.AnonymousUserGUID.ToString())
                throw new InsufficientPermissionsException( "Anonymous users cannot Update groups" );

            var result = PortalRepository.GroupUpdate(guid, callContext.User.GUID.ToGuid(), newName, newSystemPermission);

            return new ScalarResult((int) result);
        }

        #endregion
    }
}
