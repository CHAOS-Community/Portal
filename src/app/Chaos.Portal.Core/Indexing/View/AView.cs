namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cache;
    using Data.Model;
    using Solr;
    using Solr.Response;

    public abstract class AView : IView
    {
        #region Fields

        public ICache Cache;
        public IIndex Core { get; set; }

        #endregion
        #region Initialization

        protected AView(string name)
        {
            Name = name;
        }

        public IView WithCache(ICache cache)
        {
            Cache = cache;

            return this;
        }

        public IView WithIndex(IIndex index)
        {
            Core = index;

            return this;
        }

        public IView WithPortalApplication(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;

            return this;
        }

        #endregion
        #region Properties

        public IPortalApplication PortalApplication { get; set; }

        public string Name { get; private set; }

        #endregion
        #region Business Logic

        public void Index(IEnumerable<object> objectsToIndex)
        {
            var results = GetIndexResults(objectsToIndex).ToList();

            var cacheWriter = new CacheWriter(Cache);
            var cacheDocuments = results.Select(res => new CacheDocument{ Id = CreateKey(res.UniqueIdentifier.ToString()), Dto = res });
            cacheWriter.Write(cacheDocuments);
            Core.Index(results);

            cacheWriter.Commit();
        }

        private class CacheWriter
        {
            private ICache Cache { get; set; }
            private IList<CacheDocument> CacheBuffer { get; set; } 

            public CacheWriter(ICache cache)
            {
                Cache = cache;
                CacheBuffer = new List<CacheDocument>();
            }

            public void Write(IEnumerable<CacheDocument> cacheDocuments)
            {
                foreach (var doc in cacheDocuments)
                {
                    Write(doc);
                }
            }
            
            public void Write(CacheDocument cacheDocument)
            {
                CacheBuffer.Add(cacheDocument);
            }

            public void Commit()
            {
                foreach (var cacheDocument in CacheBuffer)
                {
                    Cache.Store(cacheDocument.Id, cacheDocument);
                }

                CacheBuffer.Clear();
            }
        }

        private class CacheDocument
        {
            public string Id { get; set; }
            public object Dto { get; set; }

            public string Fullname
            {
                get { return Dto.GetType().FullName; }
            }
        }

        public IEnumerable<IIndexable> GetIndexResults(IEnumerable<object> objectsToIndex)
        {
            foreach(var viewResults in objectsToIndex.Select(Index).Where(viewResults => viewResults != null))
            {
                foreach(var results in viewResults)
                {
                    yield return results;
                }
            }
        }

        public abstract IList<IViewData> Index(object objectsToIndex);

        public void Delete()
        {
            Core.Delete();
            // todo: clean cache
        }

        public void Delete(string uniqueIdentifier)
        {
            Core.Delete(uniqueIdentifier);
            Cache.Remove(CreateKey(uniqueIdentifier));
        }

        public virtual IGroupedResult<IResult> GroupedQuery(IQuery query)
        {
            throw new NotImplementedException("Grouping not implemented on this view");
        }

        public virtual FacetResult FacetedQuery(IQuery query)
        {
            var response = Core.Query(query);

            return response.FacetResult;
        }

        public virtual IPagedResult<IResult> Query(IQuery query)
        {
            throw new NotImplementedException("Querying not implemented on this view");
        }

        public IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult
        {
            var response   = Core.Query( query );
            var foundCount = response.QueryResult.FoundCount;
            var startIndex = response.QueryResult.StartIndex;
            var keys       = response.QueryResult.Results.Select( item => CreateKey( item.Id ) );
            var results    = Cache.Get<TResult>(keys);

            return new PagedResult<TResult>(foundCount, startIndex, results);
        }

        public string CreateKey(string key)
        {
            if (string.IsNullOrEmpty(Name)) throw new NullReferenceException(string.Format("Name on {0} view is null", Name));
            if (string.IsNullOrEmpty(key)) throw new NullReferenceException(string.Format("UniqueIdentifier on {0} view is null", Name));
            
            return string.Format( "{0}_{1}", Name, key.Replace( " ", "" ) );
        }

        #endregion
    }
}