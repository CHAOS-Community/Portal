using System.Linq;
using System.ServiceModel.Web;
using Geckon.Portal.Data;
using System;

namespace Geckon.Portal.Login
{
    public abstract class LoginBase : ILogin
    {
        #region Fields

        protected static readonly PortalDataContext PortalData = new PortalDataContext();

        #endregion
        #region Methods

        public abstract User Login( string sessionID, string input );
        public abstract User Login( string sessionID, IncomingWebRequestContext request );

        protected string AuthenticateSession( UserInfo userInfo, Guid userGUID )
        {
            using( PortalDataContext db = new PortalDataContext() )
            {
                return db.Session_Update( null, userGUID, userInfo.SessionID, null ).First().SessionID.ToString();
            }
        }

        #endregion

    }
}
