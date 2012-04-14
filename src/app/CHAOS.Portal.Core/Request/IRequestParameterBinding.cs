namespace CHAOS.Portal.Core.Request
{
    public interface IRequestParameterBinding
    {
        object Bind( ICallContext callContext );
    }
}
