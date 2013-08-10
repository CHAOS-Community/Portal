namespace Chaos.Portal.v6.Response
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;
    using Chaos.Portal.Core.Response.Dto.v2;
    using Chaos.Portal.Core.Response.Specification;

    public class PortalResponse2 : IPortalResponse
    {
        #region Fields

        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>();

        #endregion
        #region Initialization

        static PortalResponse2()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());
        }

        public PortalResponse2(IPortalRequest request)
        {
            WithResponseSpecification(ResponseSpecifications[request.ReturnFormat]);
            ReturnFormat = request.ReturnFormat;
            Callback     = request.Parameters.ContainsKey("callback") ? request.Parameters["callback"] : null;
            Request      = request;
            Encoding     = Encoding.UTF8;
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

		public void WriteToOutput( object obj )
        {
            
	    }

        public Stream GetResponseStream()
        {
            return ResponseSpecification.GetStream(this);
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
