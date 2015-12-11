namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    public interface IPagedResult<out TReturnType>
    {
        uint FoundCount { get; }
        uint StartIndex { get; }

        IEnumerable<TReturnType> Results { get; }
    }
}
