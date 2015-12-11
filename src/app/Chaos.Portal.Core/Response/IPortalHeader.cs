namespace Chaos.Portal.Core.Response
{
    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public interface IPortalHeader
    {
        [Serialize("Duration")]
        [SerializeXML(true)]
        double Duration { get; }
    }
}