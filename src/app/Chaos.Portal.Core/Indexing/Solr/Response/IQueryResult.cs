namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;

    public interface IQueryResult<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        string Value { get; }
        uint FoundCount { get; set; }
        uint StartIndex { get; set; }
        IEnumerable<TReturnType> Results { get; set; }

        #endregion
    }
}