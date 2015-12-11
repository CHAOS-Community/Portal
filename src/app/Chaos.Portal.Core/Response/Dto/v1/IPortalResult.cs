namespace Chaos.Portal.Core.Response.Dto.v1
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
