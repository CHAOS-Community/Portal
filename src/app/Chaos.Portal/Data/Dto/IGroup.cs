using System;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto
{
    public interface IGroup : IResult
    {
        [Serialize]
        UUID GUID { get; set; }

        [Serialize]
        long? SystemPermission { get; set; }

        [Serialize]
        string Name { get; set; }

        [Serialize]
        DateTime DateCreated { get; set; } 
    }
}