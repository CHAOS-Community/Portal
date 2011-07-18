using System;
using System.Configuration;
using System.Linq;
using Geckon.Portal.Data;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class TestBase
    {
        #region Properties

        public Session Session { get; set; }
        public User    User { get; set; }

        #endregion
        #region Constructions

        [SetUp]
        public void SetUp()
        {
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                db.PopulateWithDefaultData();
                User    = db.User_Insert(null, "Firstname", "Middlename", "Lastname", "Email@Email.com").First();
                Session = db.Session_Insert( Guid.NewGuid(), User.GUID, 1 ).First();
            }
        }

        [TearDown]
        public void TearDown()
        {
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                db.User_Delete( User.GUID );
                db.Session_Delete( Session.SessionID, null, null );
            }
        }

        #endregion
    }
}
