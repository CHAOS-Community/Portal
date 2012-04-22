using CHAOS.Portal.Core.Module;

namespace CHAOS.Portal.Core.Test
{
    public class MockModule : AModule
    {
        public override void Initialize(string configuration)
        {
            
        }

        [Datatype("Mock","Test")]
        public StringResult Test(ICallContext callContext)
        {
            return new StringResult( "Test" );
        }
    }
}
