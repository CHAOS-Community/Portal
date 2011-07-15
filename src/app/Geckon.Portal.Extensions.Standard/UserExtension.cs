using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
        #region Create Password (Email/Password)
        
        public ContentResult CreatePassword( string sessionID, string userGUID, string password )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                UserInfo user = db.UserInfo_Get( Guid.Parse( userGUID ), null, null, null, null ).First();

                
            }
        }

        #endregion
        #region Login (Email/password)

        public ContentResult LoginEmailPassword( string sessionID, string email, string password )
        {
            SHA1Managed sha1 = new SHA1Managed();

            byte[] byteHash =  sha1.ComputeHash( Encoding.UTF8.GetBytes( password ) );

            string hash = BitConverter.ToString( byteHash );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.UserInfo user = Data.Dto.UserInfo.Create( db.UserInfo_Get( null, null, email, hash, EmailPasswordAuthenticationProviderID ).First() );
                db.Session_Update( null, user.GUID, null, Guid.Parse( sessionID ), null, null ).First();
                
                ResultBuilder.Add( "Geckon.Portal",
                                   user );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "email", email ), 
                         new Parameter( "password", password ) );

            return GetContentResult();
        }

        private Guid? _EmailPasswordAuthenticationProviderID;
        protected Guid? EmailPasswordAuthenticationProviderID
        {
            get
            {
                if( _EmailPasswordAuthenticationProviderID == null )
                    _EmailPasswordAuthenticationProviderID = Guid.Parse( ConfigurationManager.AppSettings["AuthenticationProvider_EmailPassword_GUID"] );

                return _EmailPasswordAuthenticationProviderID;
            }
        }

        #endregion
    }
}
