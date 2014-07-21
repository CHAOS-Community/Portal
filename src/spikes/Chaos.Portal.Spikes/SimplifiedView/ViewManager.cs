namespace Chaos.Portal.Spikes.SimplifiedView
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ViewManager
    {
        readonly IDictionary<string, ViewInfo> _viewFactories = new Dictionary<string, ViewInfo>(); 

        public void AddView(string name, Func<IView> viewFactory)
        {
            var viewInfo = new ViewInfo
                {
                    ViewFactory = viewFactory,
                    Cache = null,
                    Searcher = null
                };
            _viewFactories.Add(name, viewInfo);
        }

        public IView GetView(string name)
        {
            return _viewFactories[name].ViewFactory.Invoke();
        }

        public void Index(IEnumerable<object> objects)
        {
            Parallel.ForEach(_viewFactories.Values, new ParallelOptions{MaxDegreeOfParallelism = 4}, viewInfo =>
                {
                    var view = viewInfo.ViewFactory.Invoke();

                    foreach (var obj in objects)
                    {
                        view.Index(obj, viewInfo.Cache, viewInfo.Searcher);
                    }

                    viewInfo.Cache.Commit();
                    viewInfo.Searcher.Commit();
                });
        }
    }
}