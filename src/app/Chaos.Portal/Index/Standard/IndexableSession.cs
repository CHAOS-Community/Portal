namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Linq;

    using Chaos.Portal.Cache.Couchbase;
    using Chaos.Portal.Data.Dto;

    using CHAOS;
    using CHAOS.Index;

    /// <summary>
    /// The indexable session
    /// </summary>
    public class IndexableSession : IIndexable, IIndexResult, ICacheable
    {
        #region Initialize

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexableSession"/> class.
        /// </summary>
        /// <param name="session">
        /// The <see cref="ISession"/> instance to map
        /// </param>
        public IndexableSession(ISession session)
        {
            UniqueIdentifier = new KeyValuePair<string, string>("Guid", session.Guid.ToString());
            UserGuid         = session.UserGuid;
            DateModified     = session.DateModified;
            DateCreated      = session.DateCreated;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexableSession"/> class.
        /// </summary>
        public IndexableSession()
        {
        }

        /// <summary>
        /// Initializes the object from xml
        /// </summary>
        /// <param name="element">
        /// The element.
        /// </param>
        /// <returns>
        /// The <see cref="IIndexResult"/>.
        /// </returns>
        public IIndexResult Init(XElement element)
        {
            return this;
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public KeyValuePair<string, string> UniqueIdentifier { get; private set; }

        /// <summary>
        /// Gets or sets the user guid.
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the document id.
        /// </summary>
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

        #endregion
        /// <summary>
        /// The get indexable fields.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return UniqueIdentifier;
            yield return new KeyValuePair<string, string>("UserGuid", this.UserGuid.ToString());
            yield return new KeyValuePair<string, string>("DateCreated", DateCreated.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));

            if (DateModified.HasValue) yield return new KeyValuePair<string, string>("DateModified", DateModified.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));
        }
    }
}