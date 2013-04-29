namespace Chaos.Portal.Extension
{
    using System;

    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Data.Model;

    [PortalExtension( configurationName : "Portal" )]
    public class ClientSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration( string configuration ) { return this; }

        #endregion
        #region Get

        public Core.Data.Model.ClientSettings Get( Guid guid )
        {
            return PortalRepository.ClientSettingsGet(guid);
        }

        #endregion
        #region Set

        public uint Set( Guid guid, string name, string settings )
        {
            if(!User.HasPermission(SystemPermissons.Manage)) throw new InsufficientPermissionsException( "User does not have manage permissions" );

            return PortalRepository.ClientSettingsSet(guid, name, settings);
        }

        #endregion
    }
}
