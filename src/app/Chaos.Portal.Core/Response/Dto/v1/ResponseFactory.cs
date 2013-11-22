namespace Chaos.Portal.Core.Response.Dto.v1
{
    using System;
    using System.Collections.Generic;
    using Data.Model;
    using Exceptions;
    using Request;

    public class ResponseFactory
    {

        public object Create(object obj, IPortalRequest request, PortalResponse portalResponse)
        {
            var moduleName = "Portal";
            var namedResult = obj as NamedResult;

            if (namedResult != null)
            {
                moduleName = namedResult.ModuleName;
                obj = namedResult.Obj;
            }

            return CreateWithModuleName(obj, request, moduleName, portalResponse);
        }

        private static object CreateWithModuleName(object obj, IPortalRequest request, string moduleName, PortalResponse portalResponse)
        {
            if (obj == null) throw new NullReferenceException("Returned object is null");

            var result = obj as IResult;
            var results = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var groupedResult = obj as IGroupedResult<IResult>;
            var uinteger = obj as uint?;
            var integer = obj as int?;
            var exception = obj as Exception;
            var attachment = obj as Attachment;

            if (result != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(result);

                return response;
            }
            if (results != null)
            {
                var response = new PortalResult(request.Stopwatch);

                foreach (var item in results) response.GetModule(moduleName).AddResult(item);

                return response;
            }
            if (pagedResult != null)
            {
                var response = new PortalResult(request.Stopwatch);

                foreach (var item in pagedResult.Results) response.GetModule(moduleName).AddResult(item);

                response.GetModule(moduleName).TotalCount = pagedResult.FoundCount;

                return response;
            }
            if (uinteger != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(new ScalarResult((int) uinteger.Value));

                return response;
            }
            if (integer != null)
            {
                var response = new PortalResult(request.Stopwatch);
                response.GetModule(moduleName).AddResult(new ScalarResult(integer.Value));

                return response;
            }
            if (attachment != null)
            {
                portalResponse.SetHeader("Content-Disposition", string.Format("filename={0};", attachment.FileName));
                portalResponse.SetHeader("Content-Type", attachment.ContentType);

                portalResponse.ReturnFormat = ReturnFormat.ATTACHMENT;

                return attachment.Stream;
            }
            else
            {
                if (exception == null)
                    exception =
                        new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);
                if (groupedResult != null)
                    exception =
                        new UnsupportedExtensionReturnTypeException("This Action is not available with the current Format");
                var response = new PortalResult(request.Stopwatch);

                response.GetModule(moduleName).AddResult(new ExtensionError(exception, request.Stopwatch));

                return response;
            }
        }
    }
}