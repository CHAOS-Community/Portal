using System;
using CHAOS.Portal.Data.DTO;
using Geckon.Index;
using Geckon.Portal.Core.Cache;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        UUID SessionGUID { get; set; }
		UserInfo User { get; }
        UUID AnonymousUserGUID { get; }
        ICache Cache { get; }
        IIndexManager IndexManager { get; }
    }
}
