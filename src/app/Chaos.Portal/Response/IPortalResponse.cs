using System.IO;

namespace Chaos.Portal.Response
{
    public interface IPortalResponse
    {
        IPortalError Error { get; set; }
        IPortalHeader Header { get; set; }
        IPortalResult Result { get; set; }
        Stream Stream { get; set; }

        IPortalResponse WithResponseSpecification(IResponseSpecification responseSpecification);
        void WriteToResponse( object obj );
        System.IO.Stream GetResponseStream();
    }
}