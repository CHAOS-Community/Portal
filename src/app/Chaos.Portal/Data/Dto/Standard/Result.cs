using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace Chaos.Portal.Data.Dto.Standard
{
    [Serialize("Result")]
    public abstract class Result : IResult
    {
        #region Fields

        private string _fullname;

        #endregion
        #region Properties

        [SerializeXML(true)]
        [Serialize("FullName")]
        public string Fullname
        {
            get { return _fullname ?? (_fullname = GetType().FullName); }
            set
            {
                _fullname = value;
            } 
        }

        #endregion
    }
}
