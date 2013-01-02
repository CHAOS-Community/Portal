using System.Collections.Generic;
using System.IO;
using CHAOS.Portal.Exception;
using CHAOS.Serialization;
using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Response
{
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
            var result      = obj as IResult;
            var results     = obj as IEnumerable<IResult>;
            var pagedResult = obj as IPagedResult<IResult>;
            var stream      = obj as Stream;

		    if( result != null )
                Result.Results.Add(result);
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
		    else 
            if( stream != null )
		    {
		        
		    }
            else
		        throw new UnsupportedModuleReturnTypeException(
		            "Only a return type of IResult, IEnumerable<IResult> or PagedResult<IResult> is supported, type was: " +
		            obj.GetType().FullName);
	    }

        public Stream GetResponseStream()
        {
            return ResponseSpecification.GetStream(this);
        }

		#endregion
	}
}
