namespace Chaos.Portal.Core.Data.Model
{
    using System;

    [Flags]
    public enum SystemPermissons : uint
    {
        None               = 0,
        CreateGroup        = 1 << 0,
        CreateSubscription = 1 << 1,
        Manage             = 1 << 2,
        UserManager        = 1 << 3,
        All                = uint.MaxValue
    }
}