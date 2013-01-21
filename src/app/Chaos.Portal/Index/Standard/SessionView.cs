namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Chaos.Portal.Data.Dto;

    using CHAOS;
    using CHAOS.Index;
    using CHAOS.Index.Solr;

    using Chaos.Portal.Data.Dto.Standard;

    public class SessionView : IView
    {
        #region Fields

        private readonly IIndex _index;

        #endregion
        #region Initialize

        public SessionView()
        {
            _index = new Solr { Cores = new List<SolrCoreConnection> { new SolrCoreConnection("http://172.18.5.1:8080/solr/core1") } };
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="objs">
        /// The objects.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IViewReport</cref>
        ///     </see> .
        /// </returns>
        public IViewReport Index(IEnumerable<object> objs)
        {
            var sessions = objs.OfType<ISession>().ToList();

            _index.Set(sessions.Select(Index));

            return new ViewReport { NumberOfIndexedDocuments = (uint)sessions.Count};
        }

        public IIndexable Index(ISession session)
        {
            return new IndexableSession(session);
        }

        public IEnumerable<IIndexResult> Query(IQuery query)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class IndexableSession : IIndexable
    {
        #region Initialize

        public IndexableSession(ISession session)
        {
            UniqueIdentifier = new KeyValuePair<string, string>("Guid", session.GUID.ToString());
            UserGUID         = session.UserGUID;
            DateModified     = session.DateModified;
            DateCreated      = session.DateCreated;
        }

        #endregion

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return UniqueIdentifier;
            yield return new KeyValuePair<string, string>("UserGuid", UserGUID.ToString());
            yield return new KeyValuePair<string, string>("DateCreated", DateCreated.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));

            if (DateModified.HasValue) yield return new KeyValuePair<string, string>("DateModified", DateModified.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));
        }

        public KeyValuePair<string, string> UniqueIdentifier { get; private set; }

        public UUID UserGUID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}