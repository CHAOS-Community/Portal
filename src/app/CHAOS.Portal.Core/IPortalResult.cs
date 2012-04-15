using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace CHAOS.Portal.Core
{
    public interface IPortalResult
    {
        [Serialize("Duration")]
        [SerializeXML(true)]
        long Duration { get; }

        IModuleResult GetModule( string modulename );
    }
}
