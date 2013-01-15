namespace Chaos.Portal.Logging
{
    public interface ILogFactory
    {
        ILog Create();
        ILogFactory WithLogLevel(LogLevel logLevel);
    }
}