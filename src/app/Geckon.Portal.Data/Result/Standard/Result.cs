using Geckon.Serialization;

namespace Geckon.Portal.Data.Result.Standard
{
    public abstract class Result : IResult
    {
        [Serialize("Fullname")]
        public virtual string Fullname
        {
            get { return GetType().FullName; }
        }
    }
}
