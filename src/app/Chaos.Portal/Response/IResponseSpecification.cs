using System.IO;

namespace Chaos.Portal.Response
{
    public interface IResponseSpecification
    {
        Stream GetStream(IPortalResponse response);
    }
}