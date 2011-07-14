using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserExtension : AExtension
    {
        #region Constructor

        public UserExtension()
        {
        }

        public UserExtension( IPortalContext context ) : base(context)
        {
        }

        #endregion
        #region Get

        public ContentResult Get( string sessionID )
        {
            Data.Dto.UserInfo userInfo = PortalContext.Cache.Get<Data.Dto.UserInfo>( string.Format( "[UserInfo:sid={0}]", sessionID ) );

            if( userInfo == null )
            {
                using( PortalDataContext db = GetNewPortalDataContext() )
                {
                    userInfo = Data.Dto.UserInfo.Create( db.UserInfo_Get( null, Guid.Parse( sessionID ) ).First() );

                    PortalContext.Cache.Put( string.Format( "[UserInfo:sid={0}]", sessionID ),
                                             userInfo.ToXML().OuterXml,
                                             new TimeSpan( 0, 1, 0 ) );
                }
            }

            ResultBuilder.Add( "Geckon.Portal", userInfo );

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }

        #endregion
        #region Create
        
        #endregion
    }
}
