namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Chaos.Portal.Data.Dto;

    using CHAOS.Index;
    using CHAOS.Index.Solr;

    using Chaos.Portal.Data.Dto.Standard;

    /// <summary>
    /// This view is responsible for indexing and querying groups
    /// </summary>
    public class GroupView : IView
    {
        #region Fields

        /// <summary>
        /// The _index.
        /// </summary>
        private readonly IIndex _index;

        #endregion
        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupView"/> class.
        /// </summary>
        public GroupView()
        {
            this._index = new Solr { Cores = new List<SolrCoreConnection> { new SolrCoreConnection("http://172.18.5.1:8080/solr/core0") } };
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Attempts to _index the objects.
        /// </summary>
        /// <param name="objs">
        /// The objects to _index.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IViewReport</cref>
        ///     </see> .
        /// </returns>
        public IViewReport Index(IEnumerable<object> objs)
        {
            var groups = objs.OfType<IGroup>().ToList();

            this._index.Set(groups.Select(Index));

            return new ViewReport { NumberOfIndexedDocuments = (uint)groups.Count };
        }

        /// <summary>
        /// The _index.
        /// </summary>
        /// <param name="group">
        /// The group.
        /// </param>
        /// <returns>
        /// The <see cref="IIndexable"/>.
        /// </returns>
        public IIndexable Index(IGroup group)
        {
            return new IndexableGroup(group);
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// Returns a <see>
        ///       <cref>IEnumerable</cref>
        ///     </see> containing the query result.
        /// </returns>
        public IEnumerable<IIndexResult> Query(IQuery query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class IndexableGroup : IIndexable
    {
        #region Initialize

        public IndexableGroup(IGroup group)
        {
            UniqueIdentifier = new KeyValuePair<string, string>("Guid", group.GUID.ToString());
            Name             = group.Name;
            SystemPermission = group.SystemPermission ?? 0;
            DateCreated      = group.DateCreated;
        }

        #endregion

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return UniqueIdentifier;
            yield return new KeyValuePair<string, string>("Name", Name);
            yield return new KeyValuePair<string, string>("SystemPermission", SystemPermission.ToString());
            yield return new KeyValuePair<string, string>("DateCreated", DateCreated.ToString( "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture ) );
        }

        public KeyValuePair<string, string> UniqueIdentifier { get; private set; }
        public long SystemPermission { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
