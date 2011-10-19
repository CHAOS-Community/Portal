using System.Collections.Generic;
using Geckon.Portal.Data;
using System;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        Guid? SessionID { get; set; }
        UserInfo User { get; }
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        IIndex Solr { get; }
    }
}
