using System.Collections.Generic;
using Geckon.Portal.Data;
using System;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        Guid? SessionID { get; set; }
        UserInfo User { get; }
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        ISolr Solr { get; }
        IEnumerable<Parameter> Parameters { get; set; }
    }
}
