namespace CHAOS.Portal.Core.Module
{
    public interface IModule
    {
        void Initialize( string configuration );
        void CallAction( ICallContext callContext );
    }
}
