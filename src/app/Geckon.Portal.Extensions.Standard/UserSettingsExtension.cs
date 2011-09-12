﻿using System;
using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserSettingsExtension : AExtension
    {
        #region GET

        public void Get( string sessionID, string clientSettingsGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                ResultBuilder.Add( "Geckon.Portal", db.UserSettings_Get( CallContext.User.ID, null, Guid.Parse( clientSettingsGUID ) ).First() );
            }
        }

        #endregion
        #region CREATE

        public void Create( string sessionID, string clientSettingsGUID, string settings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Create( CallContext.User.ID, null, Guid.Parse( clientSettingsGUID ), XElement.Parse( settings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                ResultBuilder.Add( "Geckon.Portal", db.UserSettings_Get( CallContext.User.ID, null, Guid.Parse( clientSettingsGUID ) ).First() );
            }
        }

        #endregion
        #region UPDATE

        public void Update( string sessionID, string clientSettingsGUID, string newSettings )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Update( CallContext.User.ID, null, Guid.Parse( clientSettingsGUID ), XElement.Parse( newSettings ) );

                if( result == -10 )
                    throw new InvalidProtocolException(  );

                ResultBuilder.Add( "Geckon.Portal", new ScalarResult( result ) );
            }
        }

        #endregion
        #region DELETE

        public void Delete( string sessionID, string clientSettingsGUID )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.UserSettings_Delete( CallContext.User.ID, null, Guid.Parse( clientSettingsGUID ) );

                if( result == -10 )
                    throw new InvalidProtocolException();

                ResultBuilder.Add( "Geckon.Portal", new ScalarResult( result ) );
            }
        }

        #endregion
    }
}
