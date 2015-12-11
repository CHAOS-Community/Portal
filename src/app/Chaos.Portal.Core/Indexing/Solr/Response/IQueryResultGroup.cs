namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public interface IQueryResultGroup<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        string Name { get; }
        uint   FoundCount { get; }

        IList<IQueryResult<TReturnType>> Groups { get; }

        #endregion
    }
}