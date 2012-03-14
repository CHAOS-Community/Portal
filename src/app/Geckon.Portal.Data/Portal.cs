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
