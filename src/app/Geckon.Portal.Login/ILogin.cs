using System.ServiceModel.Web;
using Geckon.Portal.Data;

namespace Geckon.Portal.Login
{
    public interface ILogin
    {
        User Login( string sessionID, string input );
        User Login( string sessionID, IncomingWebRequestContext request );
    }
}
