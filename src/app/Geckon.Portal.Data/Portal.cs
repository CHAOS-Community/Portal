using System.Configuration;

namespace Geckon.Portal.Data
{
    public partial class User : Dto.User { }

    public partial class Session : Dto.Session { }

    public partial class UserInfo : Dto.UserInfo { }

    public partial class PortalDataContext
    {
        public static PortalDataContext Default()
        {
            return new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString );
        }
    }
}
