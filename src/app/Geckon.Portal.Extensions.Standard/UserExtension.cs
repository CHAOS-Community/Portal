using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
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
            ResultBuilder.Add( "Geckon.Portal", GetUserInfo( sessionID ) );

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }
        
        #endregion
        #region Create
        
        public ContentResult Create( string sessionID, string firstname, string middlename, string lastname, string email )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.User user = Data.Dto.User.Create( db.User_Insert( null, firstname, middlename, lastname, email ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   user );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "firstname", firstname ),
                         new Parameter( "middlename", middlename ),
                         new Parameter( "lastname", lastname ),
                         new Parameter( "email", email ) );

            return GetContentResult();
        }

        #endregion
    }
}
