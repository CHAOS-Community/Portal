namespace Chaos.Portal.Test
{
    using Chaos.Portal.Extension;
    using Chaos.Portal.Response;

    public class ExtensionMock : AExtension
    {
        #region Overrides of AExtension

        public override IExtension WithConfiguration(string configuration)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        public int test()
        {
            return 1;
        }
    }
}