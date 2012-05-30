using System.Collections.Generic;
using CHAOS.Index;

namespace CHAOS.Portal.Test
{
    public class MockSolr : IIndex
    {
        public void Set(IIndexable item, bool commit)
        {
         
        }

        public IPagedResult<IIndexResult> Get(IQuery query)
        {
            return null;
        }

        public void Set(IIndexable item)
        {

        }

        public void Set(IEnumerable<IIndexable> items, bool commit)
        {
            
        }

        public void Set(IEnumerable<IIndexable> items)
        {
        }
    }
}