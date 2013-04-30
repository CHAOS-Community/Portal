using System.Collections.Generic;
using System.IO;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    using System;
    using System.Text;

    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response.Dto;
    using Chaos.Portal.Response.Specification;

    public class PortalResponse : IPortalResponse
    {
        #region Fields

        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>();

        #endregion
        #region Initialization

        static PortalResponse()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());
        }

        public PortalResponse(IPortalRequest request)
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

		public void WriteToResponse( object obj )
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var result      = obj as IResult;
            var results     = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var uinteger    = obj as uint?;
            var integer     = obj as int?;
            var stream      = obj as Stream;
            var exception   = obj as Exception;

		    if( result != null )
		    {
		        var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
                response.Result.Results.Add(result);

		        Output = response;
		    }
            else
            if( results != null )
            {
                var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());

                foreach (var item in results) response.Result.Results.Add(item);

                Output = response;
            }
		    else
            if( pagedResult != null )
		    {
                var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
                
                foreach (var item in pagedResult.Results) response.Result.Results.Add(item);

                response.Result.TotalCount = pagedResult.FoundCount;

                Output = response;
		    }
            else if (stream != null)
            {
                Output = stream;
            }
            else if (uinteger != null)
            {
                var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
                response.Result.Results.Add(new ScalarResult((int)uinteger.Value));

                Output = response;
            }
            else if (integer != null)
            {
                var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
                response.Result.Results.Add(new ScalarResult(integer.Value));

                Output = response;
            }
            else if(exception != null)
            {
                var response = new Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
                response.Error.SetException(exception);

                Output = response;
            }
            else throw new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);
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
