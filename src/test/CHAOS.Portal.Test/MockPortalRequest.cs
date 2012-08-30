using System.Collections.Generic;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Test
{
    public class MockPortalRequest : IPortalRequest
    {
        public string Extension
        {
            get { throw new System.NotImplementedException(); }
        }

        public string Action
        {
            get { throw new System.NotImplementedException(); }
        }

        public IDictionary<string, string> Parameters
        {
            get { throw new System.NotImplementedException(); }
        }

	    public IEnumerable<FileStream> Files { get; private set; }
    }
}
