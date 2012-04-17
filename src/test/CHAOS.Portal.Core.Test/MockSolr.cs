using System.Collections.Generic;
using Geckon.Index;

namespace CHAOS.Portal.Core.Test
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