namespace Chaos.Portal.v5.Response.Dto
{
    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public interface IPortalResult
    {
        [Serialize("Duration")]
        [SerializeXML(true)]
        long Duration { get; }

        IModuleResult GetModule(string modulename);
    }
}
