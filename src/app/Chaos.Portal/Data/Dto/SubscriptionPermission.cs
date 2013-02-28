namespace Chaos.Portal.Data.Dto
{
    using System;

    [Flags]
    public enum SubscriptionPermission : uint
    {
        None         = 0,
        CreateUser   = 1 << 0,
        CreateFolder = 1 << 1,
        Max          = uint.MaxValue
    }
}