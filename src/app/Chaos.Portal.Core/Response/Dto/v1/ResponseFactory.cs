using System;
using System.Collections.Generic;
using System.IO;
using Chaos.Portal.Core.Data.Model;
using Chaos.Portal.Core.Exceptions;
using Chaos.Portal.Core.Request;

namespace Chaos.Portal.Core.Response.Dto.v1
{
    public class ResponseFactory
    {

        public object Create(object obj, IPortalRequest request)
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var moduleName = "Portal";

            var result = obj as IResult;
            var results = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var groupedResult = obj as IGroupedResult<IResult>;
            var uinteger = obj as uint?;
            var integer = obj as int?;
            var attachment = obj as Attachment;
            var exception = obj as Exception;

            if(result != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(result);

                return response;
            }
            if(results != null)
            {
                var response = new PortalResult(request.Stopwatch);

                foreach (var item in results) response.GetModule(moduleName).AddResult(item);

                return response;
            }
            if(pagedResult != null)
            {
                var response = new PortalResult(request.Stopwatch);

                foreach (var item in pagedResult.Results) response.GetModule(moduleName).AddResult(item);

                response.GetModule(moduleName).TotalCount = pagedResult.FoundCount;

                return response;
            }
            if(attachment != null)
            {
                return attachment.Stream;
            }
            if(uinteger != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(new ScalarResult((int)uinteger.Value));

                return response;
            }
            if(integer != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(new ScalarResult(integer.Value));

                return response;
            }
            else
            {
                if (exception == null) exception = new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);
                if (groupedResult != null) exception = new UnsupportedExtensionReturnTypeException("This Action is not available with the current Format");
                var response = new PortalResult(request.Stopwatch);

                response.GetModule(moduleName).AddResult(new ExtensionError(exception, request.Stopwatch));

                return response;
            }
        }
    }
}