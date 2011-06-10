using System.Threading;
using System;
using Geckon.Serialization.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Geckon.Portal.Data.DTO
{   
    //[DataContract]
    [Serializable]
    public class SolrRebuildWorker //: ISerializable
    {
        #region Fields

       // [NonSerialized]
        private Exception _Exception;

        #endregion
        #region Constructor

        private SolrRebuildWorker()
        {}

        public SolrRebuildWorker(Guid? repositoryIdentifier)
        {
            RepositoryIdentifier = repositoryIdentifier;
            StartTime = DateTime.Now;
            Status = "Idle";
        }

        #endregion
        #region ISerialize implementation

        //public SolrRebuildWorker(SerializationInfo info, StreamingContext context)
        //{
        //    RepositoryIdentifier = Guid.Parse(info.GetString("RepositoryIdentifier"));

        //    //m_intVar = info.GetInt32("m_intVar");
        //    //if (VERSION > 1)
        //    //    m_stringVar = info.GetString("m_stringVar");
        //    //else
        //    //    m_stringVar = "default string";
        //}

        //public void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    info.AddValue("RepositoryIdentifier", RepositoryIdentifier);
        //}

        #endregion
        #region Properties

        [Element]
        public Guid? RepositoryIdentifier
        { get; set; }

        [Element]
        public bool IsCompleted
        {
            get { return (EndTime != null); }
        }

        [Element]
        public int ObjectsAdded
        { get; set; }

        [Element]
        public int ObjectsToAdd
        { get; set; }

        [Element]
        public DateTime StartTime
        { get; set; }

        [Element]
        public DateTime? EndTime
        { get; set; }

        [Element]
        public TimeSpan Elapsed
        {
            get { return (EndTime ?? DateTime.Now).Subtract(StartTime); }
        }

        [Element]
        public TimeSpan? TimeRemaining
        {
            get
            {
                if (IsCompleted)
                    return null;

                return new TimeSpan(
                    0, 0, 0,
                    (int)Math.Round(TimePerObject.TotalSeconds * (ObjectsToAdd - ObjectsAdded))
                    );
            }
        }

        [Element]
        public TimeSpan TimePerObject
        {
            get
            {
                return new TimeSpan(
                    0, 0, 0, 0,
                    (ObjectsAdded == 0) ? 0 : (int)Math.Round(Elapsed.TotalMilliseconds / ObjectsAdded)
                    );
            }
        }

        [Element]
        public DateTime? ExpectedEndTime
        {
            get
            {
                return (TimeRemaining == null) ? null : (DateTime?)DateTime.Now.Add(TimeRemaining.Value);
            }
        }

        [Element]
        public string Status
        { get; set; }

        [XmlIgnore]
        public Exception Exception
        {
            get { return _Exception; }
            set
            {
                EndTime = DateTime.Now;

                _Exception = value;
            }
        }

        [Element]
        public string ExceptionMessage
        {
            get
            {
                return (Exception == null)? null : Exception.Message;
            }
        }
        
        #endregion
    }
}