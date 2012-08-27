using CHAOS.Portal.DTO;

namespace CHAOS.Portal.Core
{
    public interface IPortalResponse
    {
        IPortalResult PortalResult { get; set; }
		void WriteToResponse( object result, object module );
    }
}