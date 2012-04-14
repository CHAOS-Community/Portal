namespace CHAOS.Portal.Core.Request
{
    public interface IPortalRequest
    {
        string       Extension { get; }
        string       Action { get; }
        Parameter[] Parameters { get; }
    }
}