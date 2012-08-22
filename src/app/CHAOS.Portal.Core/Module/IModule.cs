namespace CHAOS.Portal.Core.Module
{
    public interface IModule
    {
        void Initialize( string configuration );
        bool CallAction( ICallContext callContext );
    }
}
