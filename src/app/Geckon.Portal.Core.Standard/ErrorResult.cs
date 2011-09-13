using Geckon.Portal.Data.Result;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace Geckon.Portal.Core.Standard
{
    [Serialize("Error")]
    public class Error : IResult
    {
        #region Properties

        [Serialize("Fullname")]
        public string Fullname { get; private set; }

        [Serialize("Message")]
        public string Message { get; private set; }

        [SerializeXML(false, true)]
        [Serialize("Stacktrace")]
        public string Stacktrace { get; private set; }

        #endregion
        #region Construction

        public Error( System.Exception exception )
        {
            Fullname   = exception.GetType().FullName;
            Message    = exception.Message;
            Stacktrace = exception.StackTrace;
        }

        #endregion
    }
}
