using System;
using CHAOS.Portal.Data.DTO;
using Geckon.Index;
using Geckon.Portal.Core.Cache;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        Guid? SessionGUID { get; set; }
		UserInfo User { get; }
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        IIndexManager IndexManager { get; }
    }
}
