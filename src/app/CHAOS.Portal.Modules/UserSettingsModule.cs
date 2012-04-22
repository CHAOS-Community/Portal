using System;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class UserSettingsModule : AModule
    {
        #region Properties

        private string ConnectionString { get; set; }

        private PortalEntities NewPortalEntities
        {
            get
            {
                return new PortalEntities( ConnectionString );
            }
        }

        #endregion
        #region Constructors

        public override void Initialize( string configuration )
        {
            ConnectionString = XDocument.Parse(configuration).Root.Attribute( "ConnectionString" ).Value;
        }

        #endregion
        #region Business Logic

        #region Get

        [Datatype("UserSettings", "Get")]
        public DTO.Standard.UserSettings Get( ICallContext callContext, UUID clientGUID )
        {
            using( var db = NewPortalEntities )
            {
                var userSetting = db.UserSettings_Get( clientGUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().FirstOrDefault();

                if( userSetting == null )
                    throw new ArgumentOutOfRangeException( clientGUID.ToString() ,"There are no user settings for the requested user and client" );

                return userSetting;
            }
        }

        #endregion
        #region Set

        [Datatype("UserSettings", "Set")]
        public ScalarResult Set( ICallContext callContext, UUID clientGUID, string settings )
        {
            using( var db = NewPortalEntities )
            {
                int result = db.UserSettings_Set( clientGUID.ToByteArray(), callContext.User.GUID.ToByteArray(), settings );

                return new ScalarResult( result );
            }
        }

        #endregion
        #region Delete

        [Datatype("UserSettings", "Delete")]
        public ScalarResult Delete( ICallContext callContext, UUID clientGUID )
        {
            using( var db = NewPortalEntities )
            {
                var result = db.UserSettings_Delete( callContext.User.GUID.ToByteArray(), clientGUID.ToByteArray() ).First().Value;

                return new ScalarResult( result );
            }
        }

        #endregion

        #endregion
    }
}
