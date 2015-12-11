namespace Chaos.Portal.Core.Indexing
{
    public interface IQueryGroupSettings
    {
        string Field { get; set; }

        uint Limit { get; set; }
        uint Offset { get; set; }
    }
}