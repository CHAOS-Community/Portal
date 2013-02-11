namespace Chaos.Portal.Index.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml.Linq;

    using Chaos.Portal.Data.Dto;

    using CHAOS;
    using CHAOS.Index;

    /// <summary>
    /// The indexable session
    /// </summary>
    public class IndexableSession : IIndexable, IIndexResult
    {
        #region Initialize

        public IndexableSession(ISession session)
        {
            this.UniqueIdentifier = new KeyValuePair<string, string>("Guid", session.GUID.ToString());
            this.UserGUID         = session.UserGUID;
            this.DateModified     = session.DateModified;
            this.DateCreated      = session.DateCreated;
        }

        public IndexableSession()
        {
            
        }

        #endregion

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return this.UniqueIdentifier;
            yield return new KeyValuePair<string, string>("UserGuid", this.UserGUID.ToString());
            yield return new KeyValuePair<string, string>("DateCreated", this.DateCreated.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));

            if (this.DateModified.HasValue) yield return new KeyValuePair<string, string>("DateModified", this.DateModified.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'", CultureInfo.InvariantCulture));
        }

        public KeyValuePair<string, string> UniqueIdentifier { get; private set; }

        public UUID UserGUID { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public IIndexResult Init(XElement element)
        {
            throw new NotImplementedException();
        }
    }
}