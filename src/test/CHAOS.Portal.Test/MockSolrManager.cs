using System;
using CHAOS.Index;

namespace CHAOS.Portal.Test
{
    public class MockSolrManager : IIndexManager
    {

        public void AddIndex(string fullName, IIndexConnection connection)
        {
            throw new NotImplementedException();
        }

        public void AddIndex<T>(IIndexConnection connection)
        {
            throw new NotImplementedException();
        }

        public IIndex GetIndex(string fullName)
        {
            return new MockSolr();
        }

        public IIndex GetIndex<T>()
        {
            return new MockSolr();
        }
    }
}
