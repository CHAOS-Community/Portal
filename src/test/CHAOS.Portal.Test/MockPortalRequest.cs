using System.Collections.Generic;
using CHAOS.Portal.Core.Request;

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
    }
}
