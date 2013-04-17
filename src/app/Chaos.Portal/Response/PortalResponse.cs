using System.Collections.Generic;
using System.IO;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    using System;

    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Data.Model;

    [Serialize("PortalResponse")]
    public class PortalResponse : IPortalResponse
    {
        #region Properties

        [Serialize]
        public IPortalHeader Header{ get; set; }
        [Serialize]
        public IPortalResult Result { get; set; }
        [Serialize]
        public IPortalError  Error { get; set; }

        public Stream Stream { get; set; }

        private IResponseSpecification ResponseSpecification { get; set; }

        #endregion
        #region Initialization

        public PortalResponse(IPortalHeader header, IPortalResult result, IPortalError error)
        {
            Header = header;
            Result = result;
            Error  = error;
        }

        public IPortalResponse WithResponseSpecification(IResponseSpecification responseSpecification)
        {
            ResponseSpecification = responseSpecification;

            return this;
        }

        #endregion
		#region Business Logic

		public void WriteToResponse( object obj )
        {
            if(obj == null) throw new NullReferenceException("Returned object is null");

            var result      = obj as IResult;
            var results     = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var stream      = obj as Stream;
            var uinteger    = obj as uint?;
            var integer     = obj as int?;

		    if( result != null ) Result.Results.Add(result);
            else
            if( results != null )
                foreach (var item in results)
                    Result.Results.Add(item);
		    else
            if( pagedResult != null )
		    {
                foreach (var item in pagedResult.Results)
                    Result.Results.Add(item);

                Result.TotalCount  = pagedResult.FoundCount;
		    }
            else if (stream != null) Stream = stream;
            else if (uinteger != null) Result.Results.Add(new ScalarResult((int)uinteger.Value));
            else if (integer != null) Result.Results.Add(new ScalarResult(integer.Value));
            else 
		        throw new UnsupportedExtensionReturnTypeException(
		            "Return type is not supported: " +
		            obj.GetType().FullName);
	    }

        public Stream GetResponseStream()
        {
            return ResponseSpecification.GetStream(this);
        }

		#endregion
	}
}
