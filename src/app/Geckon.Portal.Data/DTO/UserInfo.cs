using System;
using Geckon.Security.Web;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    public class UserInfo : XmlSerialize, IUser
    {
        #region Properties

        [Element]
        public string SessionID
        {
            get;
            set;
        }

        [Element]
        public int ID
        {
            get;
            set;
        }

        [Element]
        public Guid GUID
        {
            get;
            set;
        }

        [Element]
        public string Firstname
        {
            get;
            set;
        }

        [Element]
        public string Middlename
        {
            get;
            set;
        }

        [Element]
        public string Lastname
        {
            get;
            set;
        }

        [Element]
        public string Email
        {
            get;
            set;
        }

        [Element]
        public string RepositoryName
        {
            get;
            set;
        }

        [Element]
        public string UserSettings
        {
            get;
            set;
        }

        [Element]
        public int? ClientSettingID
        {
            get;
            set;
        }

        #endregion

        public static UserInfo Create( Data.UserInfo userInfo )
        {
            UserInfo newUserInfo = new UserInfo();

            newUserInfo.SessionID            = userInfo.SessionID.ToString();
            newUserInfo.ID                   = userInfo.ID;
            newUserInfo.GUID                 = userInfo.GUID;
            newUserInfo.Firstname            = userInfo.Firstname;
            newUserInfo.Middlename           = userInfo.Middlename;
            newUserInfo.Lastname             = userInfo.Lastname;
            newUserInfo.Email                = userInfo.Email;
            newUserInfo.RepositoryName       = userInfo.RepositoryName;
            newUserInfo.ClientSettingID      = userInfo.ClientSettingID;

            return newUserInfo;
        }

        public void Update( User user )
        {
            SessionID            = user.SessionID ?? SessionID;
            ID                   = user.ID;
            GUID                 = user.GUID;
            Firstname            = user.Firstname ?? Firstname;
            Middlename           = user.Middlename ?? Middlename;
            Lastname             = user.Lastname ?? Lastname;
            Email                = user.Email ?? Email;
        }
    }
}
