using System;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Extensions.Standard
{
    public class EmailPasswordExtension : AExtension
    {
        #region Create Password (Email/Password)
        
        public void CreatePassword( string sessionID, string userGUID, string password )
        {
            IModuleResult result = PortalResult.GetModule("Geckon.Portal");

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                UserInfo user = db.UserInfo_Get( Guid.Parse( userGUID ), null, null, null, null ).First();

                // If other logins have been created, the sessionID has to match the user
                if( db.AuthenticationProvider_User_Join_Get( user.ID, null, null ).Count() > 0 && !user.GUID.Equals( CallContext.User.GUID ) )
                    throw new InsufficientPermissionsExcention("Users can only change their own password");

                SHA1Managed sha1 = new SHA1Managed();

                byte[] byteHash =  sha1.ComputeHash( Encoding.UTF8.GetBytes( password ) );

                string hash = BitConverter.ToString( byteHash ).Replace("-","").ToLower();

                db.User_AssociateWithAuthenticationProvider( user.GUID, EmailPasswordAuthenticationProviderGUID, hash );

                result.AddResult( user );
            }
        }

        #endregion
        #region Login (Email/password)

        public void Login( string sessionID, string email, string password )
        {
            IModuleResult result = PortalResult.GetModule( "Geckon.Portal" );
            SHA1Managed   sha1   = new SHA1Managed();

            byte[] byteHash =  sha1.ComputeHash( Encoding.UTF8.GetBytes( password ) );

            string hash = BitConverter.ToString( byteHash ).Replace("-","").ToLower();

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                UserInfo user = db.UserInfo_Get( null, null, email, hash, EmailPasswordAuthenticationProviderGUID ).FirstOrDefault();

                if( user == null )
                    throw new LoginException( "Login failed, either email or password is incorrect" );

                db.Session_Update( null, user.GUID, Guid.Parse( sessionID ), null ).First();

                result.AddResult( user );
            }
        }

        private Guid? _EmailPasswordAuthenticationProviderID;
        protected Guid? EmailPasswordAuthenticationProviderGUID
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
