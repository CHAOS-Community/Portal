using System;

namespace CHAOS.Portal.DTO.Standard
{
    [Flags]
    public enum SubscriptionPermission : uint
    {
        None         = 0,
        CreateUser   = 1 << 0,
        CreateFolder = 1 << 1,
        Max          = uint.MaxValue
    }
}