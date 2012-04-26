﻿using CHAOS.Serialization;
using CHAOS.Serialization.XML;

namespace CHAOS.Portal.DTO.Standard
{
    [Serialize("Error")]
    public class Error : IResult
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
        public Error InnerException { get; private set; }

        #endregion
        #region Construction

        public Error( System.Exception exception )
        {
            Fullname      = exception.GetType().FullName;
            Message        = exception.Message;
            Stacktrace     = exception.StackTrace;
            InnerException = exception.InnerException != null ? new Error( exception.InnerException ) : null;
        }

        #endregion
    }
}
