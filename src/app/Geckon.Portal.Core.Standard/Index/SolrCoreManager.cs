using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;
using Geckon.Portal.Core.Module;

namespace Geckon.Portal.Core.Standard
{
    public class SolrCoreManager : IIndexManager
    {
        #region Properties

        private IDictionary<string, IIndex> Indexes { get; set; }

        #endregion
        #region Constructors

        public SolrCoreManager()
        {
            Indexes = new Dictionary<string, IIndex>();
        }

        #endregion
        #region Business Logic

        public IIndex GetIndex<T>() where T : IModule
        {
            return GetIndex(typeof(T).FullName);
        }

        public IIndex GetIndex(string fullName)
        {
            return Indexes[fullName];
        }

        public void AddIndex<T>( IIndexConnection connection ) where T : IModule
        {
            AddIndex( typeof(T).FullName, connection );
        }

        public void AddIndex( string fullName, IIndexConnection connection )
        {
            Solr solr;

            if( Indexes.ContainsKey( fullName ) )
                solr = (Solr) Indexes[ fullName ];
            else
            {
                solr = new Solr();
                Indexes.Add( fullName, solr );
            }

            solr.AddCore( (SolrCoreConnection) connection );
        }

        #endregion
    }
}
