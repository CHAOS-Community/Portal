using System;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto
{
    public interface IClientSettings : IResult
    {
        [Serialize]
        UUID GUID { get; set; }

        [Serialize]
        string Name { get; set; }

        [Serialize]
        string Settings { get; set; }

        [Serialize]
        DateTime DateCreated { get; set; }
    }
}