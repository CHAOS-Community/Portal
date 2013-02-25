namespace Chaos.Portal.Extension
{
    using System;

    using Chaos.Portal.Data.Dto;

    [PortalExtension( configurationName : "Portal" )]
    public class ClientSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration( string configuration ) { return this; }

        #endregion
        #region Get

        public IClientSettings Get( ICallContext callContext, Guid guid )
        {
            return PortalRepository.ClientSettingsGet(guid);
        }

        #endregion
        #region Set

        public uint Set( ICallContext callContext, Guid guid, string name, string settings )
        {
            return PortalRepository.ClientSettingsSet(guid, name, settings);
        }

        #endregion
    }
}
