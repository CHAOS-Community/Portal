using System.Collections.Generic;
using CHAOS.Index;

namespace CHAOS.Portal.Test
{
    public class MockSolr : IIndex
    {

        public IPagedResult<IIndexResult> Get(IQuery query)
        {
            return null;
        }

        public void Set(IIndexable item)
        {

        }

        public void Set(IEnumerable<IIndexable> items)
        {
        }
    }
}