namespace Chaos.Portal.Indexing.Solr
{
    using System.Collections.Generic;

    public interface IIndex
    {
        Response<GuidResult> Query(IQuery query);

        void Index(IIndexable indexable);

        void Index(IEnumerable<IIndexable> indexables);

        void Commit(bool isSoftCommit = false);

        void Optimize();

        /// <summary>
        /// Delete all documents in core
        /// </summary>
        void Delete();
    }
}