using System.Collections.Generic;
using System.Diagnostics;
using CHAOS.Portal.DTO;
using CHAOS.Serialization;
using Chaos.Portal.Response;

namespace Chaos.Portal.Data.Dto.Standard
{
    [Serialize("PortalResult")]
    public class PortalResult : IPortalResult
    {
        #region Properties

        public uint Count { get; set; }
        public uint TotalCount { get; set; }
        public IList<IResult> Results { get; set; }

        #endregion
        #region Construction


        #endregion
        #region Business Logic



        #endregion
    }
}
