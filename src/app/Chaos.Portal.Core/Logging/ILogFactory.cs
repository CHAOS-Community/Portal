namespace Chaos.Portal.Core.Logging
{
    public interface ILogFactory
    {
        ILog Create();
        ILogFactory WithLogLevel(LogLevel logLevel);
    }
}