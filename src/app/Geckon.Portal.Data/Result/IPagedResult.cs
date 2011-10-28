using System;
using System.Collections.Generic;
using System.Linq;

namespace Geckon.Portal.Data.Result
{
    public interface IPagedResult
    {
        int FoundCount { get; }
        int StartIndex { get; }

        IEnumerable<IResult> Results { get; }
    }
}
