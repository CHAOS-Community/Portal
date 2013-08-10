namespace Chaos.Portal.Core.Response.Dto.v1
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

        public string Stacktrace { get; private set; }

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