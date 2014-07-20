namespace Chaos.Portal.Core.Indexing.Solr
{
    using System.Collections.Generic;

    using Response;

    public interface IIndex
    {
        IIndexResponse<IdResult> Query(IQuery query);

        void Index(IIndexable indexable);

        void Index(IEnumerable<IIndexable> indexables);

        void Commit(bool isSoftCommit = false);

        void Optimize();

        /// <summary>
        /// Delete all documents in core
        /// </summary>
        void Delete();

        /// <summary>
        /// Delete documents in all views based on the query
        /// </summary>
        void Delete(string uniqueIdentifier);
    }
}