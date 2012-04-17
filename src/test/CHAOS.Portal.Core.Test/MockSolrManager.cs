using System;
using Geckon.Index;

namespace CHAOS.Portal.Core.Test
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
