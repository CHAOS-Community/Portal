namespace Chaos.Portal.Core.Data.Model
{
    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    [Serialize("Result")]
    public abstract class AResult : IResult
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
