namespace Chaos.Portal.Core.Response.Dto.v2
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;

    public class PagedResult : Core.Data.Model.PagedResult<IResult>, IPortalResult
    {
        public PagedResult(uint foundCount, uint startIndex, IEnumerable<IResult> results) : base(foundCount, startIndex, results)
        {
        }
    }
}