namespace Chaos.Portal.Data.Dto
{
    using System;

    [Flags]
    public enum SystemPermissons : uint
    {
        None               = 0,
        CreateGroup        = 1 << 1,
        CreateSubscription = 1 << 2,
        Manage             = 1 << 3,
        All                = CreateGroup | CreateSubscription | Manage
    }
}