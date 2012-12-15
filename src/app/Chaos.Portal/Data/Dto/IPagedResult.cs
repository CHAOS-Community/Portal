using System.Collections.Generic;

namespace Chaos.Portal.Data.Dto
{
    public interface IPagedResult<out TReturnType>
    {
        uint FoundCount { get; }
        uint StartIndex { get; }

        IEnumerable<TReturnType> Results { get; }
    }
}
