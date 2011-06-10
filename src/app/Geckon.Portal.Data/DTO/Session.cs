using System;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    [Document("Geckon.Portal.Data.Dto.Session")]
    public class Session : XmlSerialize
    {
        #region Properties

        [Element]
        public string SessionID
        {
            get; set;
        }

        [Element]
        public int ClientSettingID
        {
            get;
            set;
        }

        public int UserID
        {
            get;
            set;
        }

        [Element]
        public DateTime DateCreated
        {
            get;
            set;
        }

        [Element]
        public DateTime DateModified
        {
            get;
            set;
        }

        #endregion

        public static Session Create( Data.Session session )
        {
            Session newSession = new Session();

            newSession.SessionID       = session.SessionID.ToString();
            newSession.ClientSettingID = session.ClientSettingID;
            newSession.UserID          = session.UserID;
            newSession.DateCreated     = session.DateCreated;
            newSession.DateModified    = session.DateModified;
            
            return newSession;
        }

        
    }
}
