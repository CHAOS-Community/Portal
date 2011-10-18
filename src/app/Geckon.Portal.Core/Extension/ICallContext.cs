using System.Collections.Generic;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Result.Standard;
using System;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        string SessionID { get; set; }
        UserInfo User { get; }
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        ISolr Solr { get; }
        IEnumerable<Parameter> Parameters { get; set; }
    }
}
