namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    public interface IViewQueryer
    {
    //    IGroupedResult<IResult> GroupedQuery(IQuery query);
    //    FacetResult FacetedQuery(IQuery query);
    //    IPagedResult<IResult> Query(IQuery query);
    //    IPagedResult<IResult> Query<TResult>(IQuery query) where TResult : class, IResult;
    }

    public interface IViewIndexer
    {
        IList<IViewData> Index(object objectsToIndex);
    }

    public interface IView : IViewQueryer, IViewIndexer
    {
    }
}