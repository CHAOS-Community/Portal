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
}