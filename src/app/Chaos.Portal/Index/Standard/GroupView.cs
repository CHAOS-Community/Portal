using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CHAOS.Index;
using CHAOS.Index.Solr;
using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Index.Standard
{
    public class GroupView : IView
    {
        #region Fields

        private readonly IIndex _index;

        #endregion
        #region Initialize

        public GroupView()
        {
            _index = new Solr { Cores = new List<SolrCoreConnection> { new SolrCoreConnection("http://172.18.5.1:8080/solr/core0") } };
        }

        #endregion
        #region Business Logic

        public void Index(IEnumerable<object> objs)
        {
            _index.Set(objs.OfType<IGroup>().Select(Index));
        }

        public IIndexable Index(IGroup group)
        {
            return new IndexableGroup(group);
        }

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
