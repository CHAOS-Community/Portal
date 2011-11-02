using System.Collections.Generic;
using Geckon.Portal.Data;
using System;
using Geckon.Portal.Core.Index;
using Geckon.Portal.Core.Cache;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        Guid? SessionID { get; set; }
        UserInfo User { get; }
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        IIndexManager IndexManager { get; }
    }
}
