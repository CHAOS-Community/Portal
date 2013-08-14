namespace Chaos.Portal.Core.Response
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response.Dto.v1;
    using Chaos.Portal.Core.Response.Dto.v2;
    using Chaos.Portal.Core.Response.Specification;

    public class PortalResponse : IPortalResponse
    {
        #region Fields

        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>();
        
        private readonly string _moduleName;

        #endregion
        #region Initialization

        static PortalResponse()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.XML2, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON2, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP2, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());
        }

        public PortalResponse(IPortalRequest request, string moduleName = "Portal")
        {
            WithResponseSpecification(ResponseSpecifications[request.ReturnFormat]);
            ReturnFormat = request.ReturnFormat;
            Callback     = request.Parameters.ContainsKey("callback") ? request.Parameters["callback"] : null;
            Request      = request;
            Encoding     = Encoding.UTF8;
            _moduleName  = moduleName;
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
                    WriteToObject1(obj);
                    break;
                case ReturnFormat.XML2:
                case ReturnFormat.JSON2:
                case ReturnFormat.JSONP2:
                    WriteToObject2(obj);
                    break;
            }
        }

        private void WriteToObject2(object obj)
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var result        = obj as IResult;
            var results       = obj as IEnumerable<IResult>;
            var pagedResult   = obj as IPagedResult<IResult>;
            var groupedResult = obj as IGroupedResult<IResult>;
            var uinteger      = obj as uint?;
            var integer       = obj as int?;
            var exception     = obj as Exception;

		    if( result != null )
		    {
                var portalResult = new Dto.v2.PagedResult<IResult>(1, 0, new[] { result });
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

		        Output = response;
		    }
            else
            if( results != null )
            {
                var lst          = results.ToList();
                var portalResult = new Dto.v2.PagedResult<IResult>((uint)lst.Count, 0, lst);
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
		    else
            if( pagedResult != null )
            {
                var portalResult = new Dto.v2.PagedResult<IResult>(pagedResult.FoundCount, pagedResult.StartIndex, pagedResult.Results);
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
		    }
            else
            if (groupedResult != null)
            {
                var resultGroups = groupedResult.Groups.Select(item => new Dto.v2.ResultGroup<IResult>(item.FoundCount, item.StartIndex, item.Results){Value = item.Value}).ToList();
                var portalResult = new Dto.v2.GroupedResult<IResult> {Groups = resultGroups};
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
		    }
            else if (uinteger != null)
            {
                var portalResult = new Dto.v2.PagedResult<IResult>(1, 0, new[] { new ScalarResult((int)uinteger.Value) });
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
            else if (integer != null)
            {
                var portalResult = new Dto.v2.PagedResult<IResult>(1, 0, new[] { new ScalarResult(integer.Value) });
                var response     = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), portalResult, new PortalError());

                Output = response;
            }
            else
            {
                if (exception == null) exception = new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);

                var response = new Dto.v2.PortalResponse(new PortalHeader(Request.Stopwatch), new Dto.v2.PortalResult(), new PortalError());
                response.Error.SetException(exception);

                Output = response;
            }
        }

        private void WriteToObject1(object obj)
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var result = obj as IResult;
            var results = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var groupedResult = obj as IGroupedResult<IResult>;
            var uinteger = obj as uint?;
            var integer = obj as int?;
            var stream = obj as Stream;
            var exception = obj as Exception;

            if(result != null)
            {
                var response = new Dto.v1.PortalResult(Request.Stopwatch);
                response.GetModule(_moduleName).AddResult(result);

                Output = response;
            }
            else if(results != null)
            {
                var response = new Dto.v1.PortalResult(Request.Stopwatch);

                foreach(var item in results) response.GetModule(_moduleName).AddResult(item);

                Output = response;
            }
            else if(pagedResult != null)
            {
                var response = new Dto.v1.PortalResult(Request.Stopwatch);

                foreach(var item in pagedResult.Results) response.GetModule(_moduleName).AddResult(item);

                response.GetModule(_moduleName).TotalCount = pagedResult.FoundCount;

                Output = response;
            }
            else if(stream != null)
            {
                Output = stream;
            }
            else if(uinteger != null)
            {
                var response = new Dto.v1.PortalResult(Request.Stopwatch);
                response.GetModule(_moduleName).AddResult(new ScalarResult((int)uinteger.Value));

                Output = response;
            }
            else if(integer != null)
            {
                var response = new Dto.v1.PortalResult(Request.Stopwatch);
                response.GetModule(_moduleName).AddResult(new ScalarResult(integer.Value));

                Output = response;
            }
            else
            {
                if (exception == null) exception = new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);
                if (groupedResult != null) exception = new UnsupportedExtensionReturnTypeException("This Action is not available with the current Format");
                var response = new Dto.v1.PortalResult(Request.Stopwatch);

                response.GetModule(_moduleName).AddResult(new ExtensionError(exception, Request.Stopwatch));

                Output = response;
            }
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