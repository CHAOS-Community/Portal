using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace Chaos.Portal.Response
{
    public interface IPortalHeader
    {
        [Serialize("Duration")]
        [SerializeXML(true)]
        double Duration { get; }

        string Callback { get; set; }

        System.Text.Encoding Encoding { get; set; }
        ReturnFormat ReturnFormat { get; set; }
    }
}