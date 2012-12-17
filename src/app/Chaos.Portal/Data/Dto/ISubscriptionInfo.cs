using System;
using CHAOS;
using CHAOS.Serialization;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Data.Dto
{
    public interface ISubscriptionInfo
    {
        [Serialize]
        UUID GUID { get; set; }

        [Serialize]
        string Name { get; set; }

        [Serialize]
        DateTime DateCreated { get; set; }

        [Serialize]
        UUID UserGUID { get; set; }

        SubscriptionPermission Permission { get; set; }
    }
}