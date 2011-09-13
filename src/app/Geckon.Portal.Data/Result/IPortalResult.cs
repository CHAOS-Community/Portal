namespace Geckon.Portal.Data.Result
{
    public interface IPortalResult
    {
        [Serialization.Serialize("Duration")]
        [Serialization.XML.SerializeXML(true)]
        long Duration { get; }
    }
}
