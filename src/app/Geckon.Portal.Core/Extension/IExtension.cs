using System.Web;
using System.Web.Mvc;

namespace Geckon.Portal.Core.Extension
{
    public interface IExtension : IController
    {
        IPortalContext PortalContext { get; }
        HttpContextBase HttpContext { get; }
    }
}
