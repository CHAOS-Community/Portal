namespace Chaos.Portal.Response.Dto
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public class PortalError : IPortalError
    {
        #region Properties

        [SerializeXML(true)]
        [Serialize("Fullname")]
        public string Fullname { get; private set; }

        [Serialize("Message")]
        public string Message { get; private set; }

        [SerializeXML(false, true)]
        [Serialize("Stacktrace")]
        public string Stacktrace { get; private set; }

        [Serialize("InnerException")]
        public PortalError InnerException { get; private set; }

		public System.Exception Exception { get; set; }

        #endregion
        #region Initialization

        public PortalError()
        {

        }

        public PortalError( Exception exception )
        {
            SetException(exception);
        }

        public void SetException(Exception exception)
        {
            Fullname       = exception.GetType().FullName;
            Message        = exception.Message;
            Stacktrace     = exception.StackTrace;
            InnerException = exception.InnerException != null ? new PortalError( exception.InnerException ) : null;
	        Exception      = exception;
        }

        #endregion
    }
}
