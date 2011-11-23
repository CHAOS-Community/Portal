using System;
using System.Configuration;
using System.Xml.Linq;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace Geckon.Portal.Data
{
    public partial class ExtensionMethod : Result.Standard.Result
    {
        #region Properties

        [Serialize("Extension")]
        public string Extension { get; set; }

        [Serialize("Method")]
        public string Method { get; set; }

        [Serialize("Parameters")]
        public string Parameters { get; set; }

        #endregion
        #region Construction

        public ExtensionMethod(string extension, string method, string parameters)
        { 
            Extension  = extension;
            Method     = method;
            Parameters = parameters;
        }

        #endregion
    }

    public partial class Extension : Result.Standard.Result
    {
        #region Properties

        [Serialize("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Serialize("Map")]
        public string pMap
        {
            get { return Map; }
            set { Map = value; }
        }

        [Serialize("Fullname")]
        public string pFullName
        {
            get { return Fullname; }
            set { Fullname = value; }
        }

        [Serialize("Path")]
        public string pPath
        {
            get { return Path; }
            set { Path = value; }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    public partial class Session : Result.Standard.Result
    {
        #region Properties

        [Serialize("SessionID")]
        public Guid pSessionID
        {
            get { return SessionID; }
            set { SessionID = value; }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        [Serialize("DateModified")]
        public DateTime pDateModified
        {
            get { return DateModified; }
            set { DateModified = value; }
        }

        #endregion
    }

    public partial class Group : Result.Standard.Result
    {
        #region Properties

        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Serialize("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Serialize("DateCreadted")]
        public DateTime pDateCreated
        {
            get { return DateCreadted; }
            set { DateCreadted = value; }
        }

        #endregion
    }

    public partial class UserInfo : Result.Standard.Result
    {
        #region Properties

        [Serialize("SessionID")]
        public Guid? pSessionID
        {
            get { return SessionID; }
            set { SessionID = value; }
        }

        [Serialize("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Serialize("Firstname")]
        public string pFirstname
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        [Serialize("Middlename")]
        public string pMiddlename
        {
            get { return Middlename; }
            set { Middlename = value; }
        }

        [Serialize("Lastname")]
        public string pLastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        [Serialize("Email")]
        public string pEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        [Serialize("SystemPermission")]
        public int? pSystemPermission
        {
            get { return SystemPermission; }
            set { SystemPermission = value; }
        }

        [Serialize("ClientSettingID")]
        public int? pClientSettingID
        {
            get { return ClientSettingID; }
            set { ClientSettingID = value; }
        }

        #endregion
    }

    public partial class TicketInfo : Result.Standard.Result
    {
        #region Properties

        public TicketType TicketType
        {
            get { return (TicketType) TicketTypeID; }
        }

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Serialize("TicketTypeID")]
        public int pTicketTypeID
        {
            get { return TicketTypeID; }
            set { TicketTypeID = value; }
        }

        [Serialize("TicketTypeName")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Serialize("XML")]
        public XElement pXML
        {
            get { return XML; }
            set { XML = value; }
        }

        [Serialize("Callback")]
        public string pCallback
        {
            get { return Callback; }
            set { Callback = value; }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        [Serialize("DateUsed")]
        public DateTime? pDateUsed
        {
            get { return DateUsed; }
            set { DateUsed = value; }
        }

        #endregion
    }

    public class ScalarResult : Result.Standard.Result
    {
        #region Properties

        [Serialize("Value")]
        public int Value { get; set; }

        #endregion
        #region Constructors

        public ScalarResult (int value )
        {
            Value = value;
        }

        public ScalarResult()
        {
        }

        #endregion
    }

    public partial class SubscriptionInfo : Result.Standard.Result
    {
        #region Properties

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Serialize("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    public partial class Module : Result.Standard.Result
    {
        #region Properties

        [Serialize("ID")]
        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Serialize("Name")]
        public string pName
        {
            get { return Name; }
            set { Name = value; }
        }

        [Serialize("Path")]
        public string pPath
        {
            get { return Path; }
            set { Path = value; }
        }

        [SerializeXML(false,true)]
        [Serialize("Configuration")]
        public string pConfiguration
        {
            get { return Configuration.Value; }
            set { Configuration = XDocument.Parse(value).Root; }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }


        #endregion
    }

    public partial class User : Result.Standard.Result
    {
        #region Properties

        [Serialize("SessionID")]
        public string SessionID { get; set; }

        public int pID
        {
            get { return ID; }
            set { ID = value; }
        }

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value; }
        }

        [Serialize("Firstname")]
        public string pFirstname
        {
            get { return Firstname; }
            set { Firstname = value; }
        }

        [Serialize("Middlename")]
        public string pMiddlename
        {
            get { return Middlename; }
            set { Middlename = value; }
        }

        [Serialize("Lastname")]
        public string pLastname
        {
            get { return Lastname; }
            set { Lastname = value; }
        }

        [Serialize("Email")]
        public string pEmail
        {
            get { return Email; }
            set { Email = value; }
        }

        #endregion
    }

    public partial class UserSetting : Result.Standard.Result
    {
        #region Properties

        [Serialize("ClientSettingGUID")]
        public Guid pClientSettingGUID
        {
            get { return ClientSettingGUID; }
            set { ClientSettingGUID = value; }
        }

        [Serialize("UserGUID")]
        public Guid pUserID
        {
            get { return UserGUID; }
            set { UserGUID = value; }
        }

        [SerializeXML(false, true)]
        [Serialize("Settings")]
        public string pSetting
        {
            get { return Setting.ToString(); }
            set { Setting = XElement.Parse( value ); }
        }

        [Serialize("DateCreated")]
        public DateTime pDateCreated
        {
            get { return DateCreated; }
            set { DateCreated = value; }
        }

        #endregion
    }

    public partial class ClientSetting : Result.Standard.Result
    {
        #region Properties

        [Serialize("GUID")]
        public Guid pGUID
        {
            get { return GUID; }
            set { GUID = value;  }
        }

        [Serialize("Title")]
        public string pTitle
        {
            get { return Title; }
            set { Title = value; }
        }

        [SerializeXML(false, true)]
        [Serialize("Settings")]
        public string pXml
        {
            get { return Xml == null ? null : Xml.ToString(); }
            set { Xml = XElement.Parse( value ); }
        }

        [Serialize("DateCreated")]
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
