using System.IO;

namespace Chaos.Portal.Response
{
    using System;
    using System.Text;

    public interface IPortalResponse : IDisposable
    {
        IPortalResponse WithResponseSpecification(IResponseSpecification responseSpecification);
        void WriteToResponse( object obj );
        Stream GetResponseStream();
        string Callback { get; set; }
        Encoding Encoding { get; set; }
        ReturnFormat ReturnFormat { get; set; }

        object Output { get; set; }
    }
}