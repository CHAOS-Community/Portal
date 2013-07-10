namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public class QueryResultGroup<TReturnType> : IQueryResultGroup<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        public string Name { get; private set; }
        public uint FoundCount { get; private set; }
        public IList<IQueryResult<TReturnType>> Groups { get; private set; }

        #endregion
        #region Initialization

        public QueryResultGroup(string name, uint foundCount, IList<IQueryResult<TReturnType>> groups)
        {
            Name       = name;
            FoundCount = foundCount;
            Groups     = groups;
        }

        #endregion
    }
}