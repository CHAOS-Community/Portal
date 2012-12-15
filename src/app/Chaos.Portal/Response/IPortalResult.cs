using System.Collections.Generic;
using CHAOS.Portal.DTO;

namespace Chaos.Portal.Response
{
    public interface IPortalResult
    {
        uint Count { get; set; }
        uint TotalCount { get; set; }
        IList<IResult> Results { get; set; }
    }
}