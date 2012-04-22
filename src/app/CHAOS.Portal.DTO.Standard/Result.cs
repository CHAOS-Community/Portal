using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace CHAOS.Portal.DTO.Standard
{
    [Serialize("Result")]
    public abstract class Result : IResult
    {
        [SerializeXML(true)]
        [Serialize("FullName")]
        public virtual string Fullname
        {
            get { return GetType().FullName; }
        }
    }
}
