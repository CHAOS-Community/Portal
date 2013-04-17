using System.Collections.Generic;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    using Chaos.Portal.Core.Data.Model;

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