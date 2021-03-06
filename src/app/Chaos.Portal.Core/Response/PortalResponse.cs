namespace Chaos.Portal.Core.Response
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Data.Model;
    using Exceptions;
    using Request;
    using Dto.v1;
    using Dto.v2;
    using Specification;

    public class PortalResponse : IPortalResponse
    {
        #region Fields

        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>();
        private static readonly Dto.v2.ResponseFactory ResponseFactoryv6 = new Dto.v2.ResponseFactory();
        private static readonly Dto.v1.ResponseFactory ResponseFactoryv5 = new Dto.v1.ResponseFactory();

        private readonly Dictionary<string, string> Headers; 

        #endregion
        #region Initialization

        static PortalResponse()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.XML2, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON2, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON3, new Json3Response());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP2, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());
        }

        public PortalResponse(IPortalRequest request)
        {
            WithResponseSpecification(ResponseSpecifications[request.ReturnFormat]);
            ReturnFormat = request.ReturnFormat;
            Callback     = request.Parameters.ContainsKey("callback") ? request.Parameters["callback"] : null;
            Request      = request;
            Encoding     = Encoding.UTF8;
            Headers = new Dictionary<string, string>();
        }

        public IPortalResponse WithResponseSpecification(IResponseSpecification responseSpecification)
        {
            ResponseSpecification = responseSpecification;

            return this;
        }

        #endregion
        #region Properties

        private IPortalRequest Request { get; set; }
        private IResponseSpecification ResponseSpecification { get; set; }
        public object Output { get; set; }
        public string Callback { get; set; }
        public Encoding Encoding { get; set; }
        public ReturnFormat ReturnFormat { get; set; }

        #endregion
        #region Business Logic

        // TODO: refactor switch into a strategy pattern or similar
        public void WriteToOutput(object obj)
        {
            switch(ReturnFormat)
            {
                case ReturnFormat.XML:
                case ReturnFormat.JSON:
                case ReturnFormat.JSONP:
                case ReturnFormat.ATTACHMENT:
                    Output = ResponseFactoryv5.Create(obj, Request, this);
                    break;
                case ReturnFormat.XML2:
                case ReturnFormat.JSON2:
                case ReturnFormat.JSON3:
                case ReturnFormat.JSONP2:
                    Output = ResponseFactoryv6.Create(obj, Request);
                    break;
            }
        }

        public Stream GetResponseStream()
        {
            return ResponseSpecification.GetStream(this);
        }

        public string GetHeader(string key)
        {
            return Headers.ContainsKey(key) ? Headers[key] : DefaultHeaderValue(key);
        }

        private string DefaultHeaderValue(string key)
        {
            return "";
        }

        public void SetHeader(string key, string value)
        {
            if (ContainsHeader(key))
                Headers[key] = value;
            else
                Headers.Add(key, value);
        }

        public bool ContainsHeader(string key)
        {
            return Headers.ContainsKey(key);
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            var disposable = Output as IDisposable;

            if (disposable != null) disposable.Dispose();
        }

        #endregion
    }
}