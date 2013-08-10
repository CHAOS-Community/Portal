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
    using Chaos.Portal.Core.Response.Dto;
    using Chaos.Portal.Core.Response.Specification;

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

		public void WriteToOutput( object obj )
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var result        = obj as IResult;
            var results       = obj as IEnumerable<IResult>;
            var pagedResult   = obj as IPagedResult<IResult>;
            var groupedResult = obj as IGroupedResult<IResult>;
            var uinteger      = obj as uint?;
            var integer       = obj as int?;
            var stream        = obj as Stream;
            var exception     = obj as Exception;

		    if( result != null )
		    {
                var portalResult = new Core.Response.Dto.PagedResult<IResult>(1, 0, new[] { result });
                var response     = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

		        Output = response;
		    }
            else
            if( results != null )
            {
                var lst          = results.ToList();
                var portalResult = new Core.Response.Dto.PagedResult<IResult>((uint)lst.Count, 0, lst);
                var response    = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
		    else
            if( pagedResult != null )
            {
                var portalResult = new Core.Response.Dto.PagedResult<IResult>(pagedResult.FoundCount, pagedResult.StartIndex, pagedResult.Results);
                var response     = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
		    }
            else
            if (groupedResult != null)
            {
                var resultGroups = groupedResult.Groups.Select(item => new Core.Response.Dto.ResultGroup<IResult>(item.FoundCount, item.StartIndex, item.Results){Value = item.Value}).ToList();
                var portalResult = new Core.Response.Dto.GroupedResult<IResult>(){Groups = resultGroups};
                var response     = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
		    }
            else if (stream != null)
            {
                Output = stream;
            }
            else if (uinteger != null)
            {
                var portalResult = new Core.Response.Dto.PagedResult<IResult>(1, 0, new[] { new ScalarResult((int)uinteger.Value) });
                var response     = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
            else if (integer != null)
            {
                var portalResult = new Core.Response.Dto.PagedResult<IResult>(1, 0, new[] { new ScalarResult(integer.Value) });
                var response     = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
            else if(exception != null)
            {
                var response = new Core.Response.Dto.PortalResponse(new PortalHeader(Request.Stopwatch), new PortalResult(), new PortalError());
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
