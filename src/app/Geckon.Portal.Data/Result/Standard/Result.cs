using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace Geckon.Portal.Data.Result.Standard
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
