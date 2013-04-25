using CHAOS.Portal.DTO;

namespace CHAOS.Portal.Core
{
    using System.IO;

    using CHAOS.Portal.DTO.Standard;

    public interface IPortalResponse
    {
        IPortalResult PortalResult { get; set; }

        Attachment Attachment { get; }

        void WriteToResponse( object result, object module );
    }
}