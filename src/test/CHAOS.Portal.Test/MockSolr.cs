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

        public void Remove(IQuery query)
        {
        }

        public void Remove(IQuery query, bool doCommit)
        {
        }

        public void Remove(IIndexable item)
        {
        }

        public void Remove(IEnumerable<IIndexable> items)
        {
        }

        public void Remove(IIndexable item, bool doCommit)
        {
        }

        public void Remove(IEnumerable<IIndexable> items, bool doCommit)
        {
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