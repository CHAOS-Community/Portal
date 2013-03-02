namespace Chaos.Portal.Indexing.View
{
    public interface IViewMapping
    {
        bool CanMap(object obj);
        IViewData Map(object obj);
    }
}