using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto
{
    public interface ISubscriptionInfo
    {
        [Serialize]
        UUID UserGUID { get; set; }
    }
}