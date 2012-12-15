using System.Collections.Generic;

namespace CHAOS.Portal.DTO
{
    public interface IPagedResult<out TReturnType>
    {
        uint FoundCount { get; }
        uint StartIndex { get; }

        IEnumerable<TReturnType> Results { get; }
    }
}
