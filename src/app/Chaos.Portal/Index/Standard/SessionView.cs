namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Cache.Couchbase;
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;

    using CHAOS;
    using CHAOS.Index;

    /// <summary>
    /// The session view.
    /// </summary>
    public class SessionView : IView
    {
        #region Fields

        /// <summary>
        /// The _index.
        /// </summary>
        private readonly IIndex _index;

        private readonly ICache _cache;

        #endregion
        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionView"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="cache">The cache object to use for caching Dtos</param>
        public SessionView(IIndex index, ICache cache)
        {
            _index = index;
            _cache = cache;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IEnumerable</cref>
        ///     </see> .
        /// </returns>
        /// <exception cref="NotImplementedException">not implemented
        /// </exception>
        public IEnumerable<IResult> Query(IQuery query)
        {
            var indexResponse = _index.Get<IndexableSession>(query);
            var documentIdList = indexResponse.QueryResult.Results.Select(item => item.DocumentID);

            return _cache.Get<Session>(documentIdList);
        }
        
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

            sessions.ForEach((session) => _cache.Store(session));

            return new ViewReport { NumberOfIndexedDocuments = (uint)sessions.Count};
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <returns>
        /// The <see cref="IIndexable"/>.
        /// </returns>
        private static IIndexable Index(ISession session)
        {
            return new IndexableSession(session);
        }

        #endregion
    }

    public class IndexableSession : IIndexable, IIndexResult, ICacheable
    {
        #region Initialize

        public IndexableSession(ISession session)
        {
            UniqueIdentifier = new KeyValuePair<string, string>("Guid", session.GUID.ToString());
            UserGUID         = session.UserGUID;
            DateModified     = session.DateModified;
            DateCreated      = session.DateCreated;
        }

        public IndexableSession()
        {
            
        }

        public IIndexResult Init(XElement element)
        {

            return this;
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

        public string DocumentID
        {
            get
            {
                return UniqueIdentifier.Value;
            }
            set
            {
                UniqueIdentifier = new KeyValuePair<string, string>("Guid", value);
            }
        }
    }
}