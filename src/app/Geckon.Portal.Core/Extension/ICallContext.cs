using Geckon.Portal.Data.Dto;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        string SessionID { get; set; }
        UserInfo User { get; }
    }
}
