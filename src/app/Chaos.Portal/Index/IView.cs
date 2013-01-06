using System.Collections.Generic;
using CHAOS.Index;

namespace Chaos.Portal.Index
{
    public interface IView
    {
        void Index(IEnumerable<object> obj);
        IEnumerable<IIndexResult> Query(IQuery query);
    }
}