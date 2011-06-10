using System.Web;
using System.Web.Mvc;

namespace Geckon.Portal.Core.Entrypoint
{
    public interface IEntrypoint : IController
    {
        IPortalContext PortalContext { get; }
        HttpContextBase HttpContext { get; }
    }
}
