using System.Data.Objects;
using System.Linq;
using CHAOS.Portal.Data.EF;
using NUnit.Framework;
using Geckon.Portal.Core.Standard.Extension;

namespace Geckon.Portal.Test
{
    [TestFixture]
    public class TestBase
    {
        #region Properties

		public CHAOS.Portal.Data.DTO.Session          AnonymousSession { get; set; }
        public CHAOS.Portal.Data.DTO.Session          AdminSession { get; set; }
		public CHAOS.Portal.Data.DTO.Group            AdminGroup { get; set; }
        public CHAOS.Portal.Data.DTO.SubscriptionInfo SubscriptionInfo { get; set; }
		public CHAOS.Portal.Data.DTO.UserSettings     UserSetting { get; set; }
		public CHAOS.Portal.Data.DTO.ClientSettings   ClientSettings { get; set; }
        public CallContext      AdminCallContext { get; set; }
        public CallContext      AnonCallContext { get; set; }

		public CHAOS.Portal.Data.DTO.UserInfo UserAnonymous { get; set; }
		public CHAOS.Portal.Data.DTO.UserInfo UserAdministrator { get; set; }

        #endregion
        #region Constructions

        [SetUp]
        public void SetUp()
        {
        	using( PortalEntities db = new PortalEntities() )
        	{
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

        		db.PreTest();
				
				db.Permission_Create("Group", 0x01, "DELETE",    "Permission to Delete Group");
				db.Permission_Create("Group", 0x02, "UPDATE", "Permission to Update Group");
				db.Permission_Create("Group", 0x04, "GET", "Permission to Get Group");
				db.Permission_Create("Group", 0x08, "ADD_USER",  "Permission to Add a User to the group");
				db.Permission_Create("Group", 0x10, "LIST_USER", "Permission to list users in the group");

				db.Permission_Create("Subscription", 0x01, "CREATE_USER", "Permission to Create new users");
				db.Permission_Create("Subscription", 0x02, "GET",         "Permission to Get Subscription");
				db.Permission_Create("Subscription", 0x04, "DELETE",      "Permission to Delete Subscription");
				db.Permission_Create("Subscription", 0x08, "UPDATE",      "Permission to Update Subscription");
				db.Permission_Create("Subscription", 0x10, "MANAGE",      "Permission to Manage Subscription");

				db.Permission_Create("System", 0x01, "CREATE_GROUP",        "Permission to Create a Group");
				db.Permission_Create("System", 0x02, "CREATE_SUBSCRIPTION", "Permission to Create a Subscription");
				db.Permission_Create("System", 0x04, "MANAGE",              "Permissoin to Manage the system");

				db.User_Create( new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), "anon@ymo.us" );
				db.User_Create( new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), "admin@domain.xx" );
				db.Session_Create( new UUID( "12345678-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() );
				db.Session_Create( new UUID( "23456789-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() );
        		db.Group_Create( new UUID( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ).ToByteArray(), "Administrators", null, int.MaxValue, errorCode );
				db.Group_AssociateWithUser( new UUID( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), int.MaxValue, null, errorCode );
				db.Subscription_Create(new UUID("9C4E8A99-A69B-41FD-B1C7-E28C54D1D304").ToByteArray(), "some subscription", new UUID("A0B231E9-7D98-4F52-885E-AF4837FAA352").ToByteArray(), errorCode);
				db.ClientSetting_Create( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray(), "NUnit", "<xml>settings</xml>" );
				db.UserSettings_Set( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), "<xml>settings</xml>" );
				db.UserSettings_Set( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), "<xml>settings</xml>" );

				UserAnonymous     = db.UserInfo_Get( new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), null, null ).ToDTO().First();
				UserAdministrator = db.UserInfo_Get( new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), null, null ).ToDTO().First();

				AnonymousSession = db.Session_Get( new UUID( "12345678-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();
				AdminSession     = db.Session_Get( new UUID( "23456789-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();

				AdminGroup = db.Group_Get( new UUID( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ).ToByteArray(), null, null ).ToDTO().First();

				SubscriptionInfo = db.SubscriptionInfo_Get( new UUID("9C4E8A99-A69B-41FD-B1C7-E28C54D1D304").ToByteArray(), UserAdministrator.GUID.ToByteArray() ).ToDTO().First();
				ClientSettings = db.ClientSettings_Get( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray() ).ToDTO().First();
				UserSetting = db.UserSettings_Get( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();
        	}

            AdminCallContext = new CallContext( new MockCache(), new MockSolrManager(), AdminSession.GUID.ToString() );
            AnonCallContext  = new CallContext( new MockCache(), new MockSolrManager(), AnonymousSession.GUID.ToString() );
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion
    }
}
