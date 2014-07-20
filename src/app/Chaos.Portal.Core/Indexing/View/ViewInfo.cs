namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Cache;
    using Data.Model;
    using Exceptions;
    using Solr;
    using Solr.Response;

    public class ViewInfo
    {
        public string Name { get; set; }
        public ICache Cache { get; set; }
        public IIndex Core { get; set; }
        public Func<IView> ViewFactory { get; set; }

        public ViewInfo(string name, ICache cache, IIndex core, Func<IView> viewFactory)
        {
            Name = name;
            Cache = cache;
            Core = core;
            ViewFactory = viewFactory;
        }

        public IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult
        {
            var response = Core.Query(query);
            var foundCount = response.QueryResult.FoundCount;
            var startIndex = response.QueryResult.StartIndex;
            var keys = response.QueryResult.Results.Select(item => CreateKey(item.Id));
            var results = Cache.Get<TResult>(keys);

            return new PagedResult<TResult>(foundCount, startIndex, results);
        }

        public virtual FacetResult FacetedQuery(IQuery query)
        {
            var response = Core.Query(query);

            return response.FacetResult;
        }

        public virtual IGroupedResult<IResult> GroupedQuery(IQuery query)
        {
            throw new NotImplementedException("Grouping not implemented on this view");
        }

        public virtual IPagedResult<IResult> Query(IQuery query)
        {
            throw new NotImplementedException("Querying not implemented on this view");
        }



        public void Delete()
        {
            Core.Delete();
            // todo: clean cache
        }

        public void Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new InvalidViewDataException("uniqueIdentifier cannot be null");

            Core.Delete(id);
            Cache.Remove(CreateKey(id));
        }

        public void Index(List<object> objectsToIndex, ICacheWriter cacheWriter)
        {
            var results = GetIndexResults(objectsToIndex).ToList();

            var cacheDocuments = results.Select(res => new CacheDocument { Id = CreateKey(res.UniqueIdentifier.ToString()), Dto = res });
            cacheWriter.Write(cacheDocuments);
            Core.Index(results);
        }

        public IEnumerable<IIndexable> GetIndexResults(IEnumerable<object> objectsToIndex)
        {
            var view = ViewFactory.Invoke();

            foreach (var viewResults in objectsToIndex.Select(view.Index).Where(viewResults => viewResults != null))
            {
                foreach (var results in viewResults)
                {
                    yield return results;
                }
            }
        }


        public string CreateKey(string key)
        {
            if (string.IsNullOrEmpty(Name)) throw new NullReferenceException(string.Format("Name on {0} view is null", Name));
            if (string.IsNullOrEmpty(key)) throw new NullReferenceException(string.Format("UniqueIdentifier on {0} view is null", Name));

            return string.Format("{0}_{1}", Name, key.Replace(" ", ""));
        }
    }
}