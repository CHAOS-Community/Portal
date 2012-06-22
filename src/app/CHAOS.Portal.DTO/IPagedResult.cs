using System.Collections.Generic;

namespace CHAOS.Portal.DTO
{
    public interface IPagedResult<out TReturnType>
    {
        int FoundCount { get; }
        int StartIndex { get; }

        IEnumerable<TReturnType> Results { get; }
    }
}
