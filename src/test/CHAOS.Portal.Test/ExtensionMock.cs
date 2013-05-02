namespace Chaos.Portal.Test
{
    using System;

    using Chaos.Portal.Extension;

    public class ExtensionMock : AExtension
    {
        public int test()
        {
            return 1;
        }

        public int error()
        {
            throw new ArgumentOutOfRangeException("Derived exceptions should also be written to output");
        }
    }
}