using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    public class ScalarResult : XmlSerialize
    {
        #region Properties

        [Element]
        public object Value { get; set; }

        #endregion
        #region Constructors

        public ScalarResult( object value )
        {
            Value = value;
        }

        #endregion
    }
}
