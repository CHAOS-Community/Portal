namespace Chaos.Portal.Core.Response
{
    using System.IO;
    using System;
    using System.Text;

    public interface IPortalResponse : IDisposable
    {
        IPortalResponse WithResponseSpecification(IResponseSpecification responseSpecification);
        void WriteToOutput( object obj );
        Stream GetResponseStream();
        string Callback { get; set; }
        Encoding Encoding { get; set; }
        ReturnFormat ReturnFormat { get; set; }

        object Output { get; set; }
    }
}