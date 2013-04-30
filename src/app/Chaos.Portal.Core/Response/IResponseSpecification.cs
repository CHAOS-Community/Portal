namespace Chaos.Portal.Core.Response
{
    using System.IO;

    public interface IResponseSpecification
    {
        Stream GetStream(IPortalResponse response);
    }
}