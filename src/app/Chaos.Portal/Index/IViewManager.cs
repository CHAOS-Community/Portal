using System.Collections.Generic;
using CHAOS.Index;

namespace Chaos.Portal.Index
{
    public interface IViewManager
    {
        void Index(object obj);
        void Index(IEnumerable<object> obj);
        IEnumerable<IIndexResult> Query(string key, IQuery query);

        void AddView(string key, IView view);
    }
}