namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;

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

        #endregion
        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionView"/> class.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public SessionView(IIndex index)
        {
            _index = index;
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
        public IEnumerable<IIndexResult> Query(IQuery query)
        {
            throw new NotImplementedException();
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