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

    // todo, add extension points
    // todo, Class violate SOLID (open/close & single responsibility)
    // todo, decide how much structure should be enforced upon View implementations (Triggers, Data assembly, Map to Index, Map to Dto) aka. Map/Reduce
    public class ViewInvoker
    {
        public string Name { get; set; }
        public ICache Cache { get; set; }
        public IIndex Core { get; set; }
        public Func<IView> ViewFactory { get; set; }

        public ViewInvoker(string name, ICache cache, IIndex core, Func<IView> viewFactory)
        {
            Name = name;
            Cache = cache;
            Core = core;
            ViewFactory = viewFactory;
        }

        public IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult
        {
            var queryResult = Core.Query(query).QueryResult;

            return ConvertToResultGroup<TResult>(queryResult);
        }

        public virtual FacetResult FacetedQuery(IQuery query)
        {
            var response = Core.Query(query);

            return response.FacetResult;
        }

        public virtual IGroupedResult<IResult> GroupedQuery<TResult>(IQuery query) where TResult : class, IResult
        {
            var groups = new List<ResultGroup<TResult>>();

            foreach (var queryResultGroup in Core.Query(query).QueryResultGroups)
                groups.AddRange(queryResultGroup.Groups.Select(ConvertToResultGroup<TResult>));

            return new GroupedResult<TResult>(groups);
        }

        private ResultGroup<TResult> ConvertToResultGroup<TResult>(IQueryResult<IdResult> queryResult) where TResult : class, IResult
        {
            var foundCount = queryResult.FoundCount;
            var startIndex = queryResult.StartIndex;
            var keys = queryResult.Results.Select(item => CreateKey(item.Id));
            var results = Cache.Get<TResult>(keys);

            return new ResultGroup<TResult>(foundCount, startIndex, results){Value = queryResult.Value};
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

        public void Index(List<object> objectsToIndex)
        {
            using (var cacheWriter = new BufferedCacheWriter(Cache))
            using (var indexWriter = new BufferedIndexWriter(Core))
            {
                var view = ViewFactory.Invoke();

                foreach (var viewResults in objectsToIndex.Select(view.Index).Where(viewResults => viewResults != null))
                {
                    foreach (var res in viewResults)
                    {
                        cacheWriter.Write(new CacheDocument { Id = CreateKey(res.UniqueIdentifier.Value), Dto = res });
                        indexWriter.Write(new IndexDocument { Id = res.UniqueIdentifier.Value, Fields = res });
                    }
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