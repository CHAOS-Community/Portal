using System.Collections.Generic;
using CHAOS.Serialization;
using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Response
{
    public interface IPortalResult
    {
        [Serialize]
        uint Count { get; }
        [Serialize]
        uint TotalCount { get; set; }
        [Serialize]
        IList<IResult> Results { get; set; }
    }
}