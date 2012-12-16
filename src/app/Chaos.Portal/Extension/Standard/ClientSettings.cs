using System;
using System.Collections.Generic;
using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Extension.Standard
{
    [PortalExtension( configurationName : "Portal" )]
    public class ClientSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration( string configuration ) { return this; }

        #endregion
        #region Get

        public IEnumerable<IClientSettings> Get( ICallContext callContext, Guid guid )
        {
            return PortalRepository.ClientSettingsGet(guid);
        }

        #endregion
        #region Set

        public uint Set( ICallContext callContext, Guid guid )
        {
            return PortalRepository.ClientSettingsSet(guid);
        }

        #endregion
    }
}
