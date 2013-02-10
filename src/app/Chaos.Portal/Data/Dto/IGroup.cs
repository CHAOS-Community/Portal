namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;
    
    public interface IGroup : IResult
    {
        [Serialize]
        Guid Guid { get; set; }

        [Serialize]
        long? SystemPermission { get; set; }

        [Serialize]
        string Name { get; set; }

        [Serialize]
        DateTime DateCreated { get; set; } 
    }
}