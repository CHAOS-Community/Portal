using System;
using Geckon.Security.Web;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    public class User : XmlSerialize, IUser
    {
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

        public Guid UniqueIdentifier
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
        public string Tag
        {
            get;
            set;
        }

        public static User Create(Data.User inUser)
        {
            User user = new User();

            user.ID = inUser.ID;
            user.UniqueIdentifier = inUser.UniqueIdentifier;
            user.Firstname = inUser.Firstname;
            user.Middlename = inUser.Middlename;
            user.Lastname = inUser.Lastname;
            user.Email = inUser.Email;

            return user;
        }
    }
}
