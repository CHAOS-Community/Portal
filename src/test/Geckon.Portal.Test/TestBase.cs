using System;
using System.Linq;
using Geckon.Portal.Data;
using NUnit.Framework;
using Geckon.Portal.Core.Standard.Extension;

namespace Geckon.Portal.Test
{
    [TestFixture]
    public class TestBase
    {
        #region Properties

        public Session          Session { get; set; }
        public Session          AdminSession { get; set; }
        public UserInfo         User { get; set; }
        public UserInfo         AdminUser { get; set; }
        public Group            AdminGroup { get; set; }
        public SubscriptionInfo SubscriptionInfo { get; set; }
        public UserSetting      UserSetting { get; set; }
        public ClientSetting    ClientSettings { get; set; }
        public CallContext      AdminCallContext { get; set; }
        public CallContext      AnonCallContext { get; set; }

        #endregion
        #region Constructions

        [SetUp]
        public void SetUp()
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                db.PopulateWithDefaultData();
                Session          = db.Session_Insert( Guid.NewGuid(), Guid.Parse( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ) ).First();
                AdminSession     = db.Session_Insert( Guid.NewGuid(), Guid.Parse( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ) ).First();
                User             = db.UserInfo_Get( Guid.Parse( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ), null, null ).First();
                AdminUser        = db.UserInfo_Get( Guid.Parse( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ), null, null ).First();
                AdminGroup       = db.Group_Get( null, Guid.Parse( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ), null, AdminUser.ID ).First();
                SubscriptionInfo = db.SubscriptionInfo_Get(null, Guid.Parse("9C4E8A99-A69B-41FD-B1C7-E28C54D1D304"), null, AdminUser.ID).First();
                ClientSettings   = (from clientSetting in db.ClientSettings where clientSetting.GUID.Equals( Guid.Parse("D157698A-86AC-4FDF-A304-F5EA9FB6E0F5") ) select clientSetting).First();  
                UserSetting      = db.UserSettings_Get( AdminUser.GUID, ClientSettings.GUID ).First();
            }

            AdminCallContext = new CallContext( new MockCache(), new MockSolrManager(), AdminSession.SessionID.ToString() );
            AnonCallContext  = new CallContext( new MockCache(), new MockSolrManager(), Session.SessionID.ToString() );
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion
    }
}
