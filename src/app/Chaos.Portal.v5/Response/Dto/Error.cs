namespace Chaos.Portal.v5.Response.Dto
{
    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    using Chaos.Portal.Core.Data.Model;

    [Serialize("Error")]
    public class Error : IResult
    {
        #region Properties

        [SerializeXML(true)]
        [Serialize("FullName")]
        public string Fullname { get; private set; }

        [Serialize("Message")]
        public string Message { get; private set; }

        [SerializeXML(false, true)]
        [Serialize("Stacktrace")]
        public string Stacktrace { get; private set; }

        [Serialize("InnerException")]
        public Error InnerException { get; private set; }

        public System.Exception Exception { get; set; }

        #endregion
        #region Construction

        public Error(System.Exception exception)
        {
            Fullname = exception.GetType().FullName;
            Message = exception.Message;
            Stacktrace = exception.StackTrace;
            InnerException = exception.InnerException != null ? new Error(exception.InnerException) : null;
            Exception = exception;
        }

        #endregion
    }
}