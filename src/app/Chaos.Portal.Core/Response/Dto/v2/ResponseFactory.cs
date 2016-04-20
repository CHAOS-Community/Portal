using System;
using System.Collections.Generic;
using System.Linq;
using Chaos.Portal.Core.Data.Model;
using Chaos.Portal.Core.Exceptions;
using Chaos.Portal.Core.Indexing.Solr.Request;
using Chaos.Portal.Core.Request;

namespace Chaos.Portal.Core.Response.Dto.v2
{
	public class ResponseFactory
	{
		public object Create(object obj, IPortalRequest request)
		{
			if (obj == null) throw new NullReferenceException("Returned object is null");

			var result = obj as IResult;
			var results = obj as IEnumerable<IResult>;
			var pagedResult = obj as IPagedResult<IResult>;
			var queryResult = obj as QueryResult;
			var groupedResult = obj as IGroupedResult<IResult>;
			var uinteger = obj as uint?;
			var integer = obj as int?;
			var attachment = obj as Attachment;
			var exception = obj as Exception;

			if (result != null)
			{
				var portalResult = new PagedResult(1, 0, new[] {result});
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (results != null)
			{
				var lst = results.ToList();
				var portalResult = new PagedResult((uint) lst.Count, 0, lst);
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (pagedResult != null)
			{
				var portalResult = new PagedResult(pagedResult.FoundCount, pagedResult.StartIndex, pagedResult.Results);
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (queryResult != null)
			{
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), queryResult, new PortalError());

				return response;
			}
			if (groupedResult != null)
			{
				var portalResult = new QueryResult {Groups = groupedResult.Groups.ToList()};
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (uinteger != null)
			{
				var portalResult = new PagedResult(1, 0, new[] {new ScalarResult((int) uinteger.Value)});
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (integer != null)
			{
				var portalResult = new PagedResult(1, 0, new[] {new ScalarResult(integer.Value)});
				var response = new PortalResponse(new PortalHeader(request.Stopwatch), portalResult, new PortalError());

				return response;
			}
			if (attachment != null)
				return attachment;
			else
			{
				if (exception == null)
					exception = new UnsupportedExtensionReturnTypeException("Return type is not supported: " + obj.GetType().FullName);

				var response = new PortalResponse(new PortalHeader(request.Stopwatch), new PortalResult(), new PortalError());
				response.Error.SetException(exception);

				return response;
			}
		}
	}
}