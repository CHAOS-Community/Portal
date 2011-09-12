using System;
using System.Configuration;
using System.Xml.Linq;
using Geckon.Security.Web;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data
{
    [Document("Geckon.Portal.Data.Session")]
    public partial class Session : XmlSerialize
    {
        #region Properties

        [Element("SessionID")]
        public Guid pSessionID
        {
            get { return SessionID; }
            set { SessionID = value; }
        }

        [Element("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        [Element("DateModified")]
        public DateTime pDateModified
        {
            get { return DateModified; }
            set { DateModified = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.Group")]
    public partial class Group : XmlSerialize
    {
        #region Properties

        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Element("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Element("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Element("DateCreadted")]
        public DateTime pDateCreated
        {
            get { return DateCreadted; }
            set { DateCreadted = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.UserInfo")]
    public partial class UserInfo : XmlSerialize
    {
        #region Properties

        [Element("SessionID")]
        public Guid? pSessionID
        {
            get { return SessionID; }
            set { SessionID = value; }
        }

        [Element("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Element("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Element("Firstname")]
        public string pFirstname
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        [Element("Middlename")]
        public string pMiddlename
        {
            get { return Middlename; }
            set { Middlename = value; }
        }

        [Element("Lastname")]
        public string pLastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        [Element("Email")]
        public string pEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        [Element("SystemPermission")]
        public int? pSystemPermission
        {
            get { return SystemPermission; }
            set { SystemPermission = value; }
        }

        [Element("ClientSettingID")]
        public int? pClientSettingID
        {
            get { return ClientSettingID; }
            set { ClientSettingID = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.ScalarResult")]
    public class ScalarResult : XmlSerialize
    {
        #region Properties

        [Element]
        public object Value { get; set; }

        #endregion
        #region Constructors

        public ScalarResult(object value)
        {
            Value = value;
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.SubscriptionInfo")]
    public partial class SubscriptionInfo : XmlSerialize
    {
        #region Properties

        [Element("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Element("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Element("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.Module")]
    public partial class Module : XmlSerialize
    {
        #region Properties

        [Element("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Element("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Element("Path")]
        public string pPath
        {
            get { return Path; }
            set { Path = value; }
        }

        [Element("Configuration",true)]
        public string pConfiguration
        {
            get { return Configuration.Value; }
            set { Configuration = XDocument.Parse(value).Root; }
        }

        [Element("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }


        #endregion
    }

    [Document("Geckon.Portal.Data.User")]
    public partial class User : XmlSerialize, IUser
    {
        #region Properties

        [Element("SessionID")]
        public string SessionID { get; set; }

        [Element("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Element("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Element("Firstname")]
        public string pFirstname
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        [Element("Middlename")]
        public string pMiddlename
        {
            get { return Middlename; }
            set { Middlename = value; }
        }

        [Element("Lastname")]
        public string pLastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        [Element("Email")]
        public string pEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.UserSettings")]
    public partial class UserSetting : XmlSerialize
    {
        #region Properties

        [Element("ClientSettingID")]
        public int pClientSettingID
        {
            get { return ClientSettingID; }
            set { ClientSettingID = value; }
        }

        [Element("UserID")]
        public int pUserID
        {
            get { return UserID; }
            set { UserID = value; }
        }

        [Element("Settings",true)]
        public string pSetting
        {
            get { return Setting.ToString(); }
            set { Setting = XElement.Parse( value ); }
        }

        [Element("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    [Document("Geckon.Portal.Data.ClientSettings")]
    public partial class ClientSetting : XmlSerialize
    {
        #region Properties

        [Element("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value;  }
        }

        [Element("Title")]
        public string pTitle
        {
            get { return Title; }
            set { Title = value; }
        }

        [Element("XML",true)]
        public string pXml
        {
            get { return Xml == null ? null : Xml.ToString(); }
            set { Xml = XElement.Parse( value ); }
        }

        [Element("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    public partial class PortalDataContext
    {
        public static PortalDataContext Default()
        {
            return new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString );
        }
    }
}
