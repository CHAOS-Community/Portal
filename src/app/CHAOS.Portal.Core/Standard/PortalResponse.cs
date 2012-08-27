using System.Collections.Generic;
using CHAOS.Portal.DTO;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Standard
{
    public class PortalResponse : IPortalResponse
    {
        #region Properties

        public IPortalResult PortalResult{ get; set; }

        #endregion
        #region Constructors

        public PortalResponse()
        {
            PortalResult = new PortalResult();
        }

        #endregion
		#region Business Logic

		public void WriteToResponse( object result, object module )
	    {
			var attributes  = module.GetType().GetCustomAttributes( typeof( PrettyNameAttribute ), true );
			var moduleName  = attributes.Length == 0 ? module.GetType().FullName : ( ( PrettyNameAttribute )attributes[0] ).PrettyName;
			var modelResult = PortalResult.GetModule( moduleName );

		    if( result is IResult )
                modelResult.AddResult( (IResult) result );
            else
                if( result is IEnumerable<IResult> )
                    modelResult.AddResult( (IEnumerable<IResult>) result );
                else
                    if( result is IPagedResult<IResult> )
                    {
                        var pagedResult = (IPagedResult<IResult>) result;

                        modelResult.AddResult( pagedResult.Results );
                        modelResult.TotalCount = pagedResult.FoundCount;
                    }
                    else
                        throw new UnsupportedModuleReturnTypeException( "Only a return type of IResult, IEnumerable<IResult> or PagedResult<IResult> is supported" );
	    }

		#endregion
	}
}
