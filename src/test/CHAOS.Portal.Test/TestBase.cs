using System.Data.Objects;
using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.Data.EF;
using NUnit.Framework;

namespace CHAOS.Portal.Test
{
    [TestFixture]
    public abstract class TestBase
    {
        #region Properties

        public PortalApplication PortalApplication { get; set; }

        public DTO.Standard.Session AnonymousSession { get; set; }
        public DTO.Standard.Session AdminSession { get; set; }
        public DTO.Standard.Group AdminGroup { get; set; }
        public DTO.Standard.SubscriptionInfo SubscriptionInfo { get; set; }
        public DTO.Standard.UserSettings UserSetting { get; set; }
        public DTO.Standard.ClientSettings ClientSettings { get; set; }
        public ICallContext AdminCallContext { get; set; }
        public ICallContext AnonCallContext { get; set; }

        public DTO.Standard.UserInfo UserAnonymous { get; set; }
        public DTO.Standard.UserInfo UserAdministrator { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            using( var db = new PortalEntities() )
        	{
				var errorCode = new ObjectParameter( "ErrorCode", 0 );

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
                db.Module_Create( 1, "Portal", "<Settings ConnectionString=\"metadata=res://*/Portal.csdl|res://*/Portal.ssdl|res://*/Portal.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=10.211.55.9;User Id=Portal;password=GECKONpbvu7000;database=Portal;Persist Security Info=True&quot;\" />" );
                db.Module_Create( 2, "MCM", "<Settings ConnectionString=\"metadata=res://*/MCM.csdl|res://*/MCM.ssdl|res://*/MCM.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=10.211.55.9;User Id=Portal;password=GECKONpbvu7000;Persist Security Info=True;database=MCM&quot;\" />" );
                db.Module_Create( 3, "EmailPassword", "<Settings ConnectionString=\"metadata=res://*/EmailPasswordDB.csdl|res://*/EmailPasswordDB.ssdl|res://*/EmailPasswordDB.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=10.211.55.9;User Id=Portal;password=GECKONpbvu7000;Persist Security Info=True;database=EmailPassword&quot;\" ChangePasswordRequestSubject=\"Password change request\" SMTPPassword=\"NOREPLYpbvu7000\" FromEmailAddress=\"no-reply@chaos-community.org\"><ChangePasswordRequestEmail><![CDATA[<?xml version=\"1.0\" encoding=\"UTF-16\"?><html xsl:version=\"1.0\" xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" xmlns=\"http://www.w3.org/1999/xhtml\"><body><a><xsl:attribute name=\"href\"><xsl:value-of select=\"//URL\"/>&amp;TicketGUID=<xsl:value-of select=\"//TicketInfo/TicketGUID\"/></xsl:attribute>Follow the white rabbit</a></body></html>]]></ChangePasswordRequestEmail></Settings>");
                db.Module_Create( 4, "SecureCookie", "<Settings ConnectionString=\"metadata=res://*/SecureCookieDB.csdl|res://*/SecureCookieDB.ssdl|res://*/SecureCookieDB.msl;provider=MySql.Data.MySqlClient;provider connection string=&quot;server=10.211.55.9;User Id=Portal;password=GECKONpbvu7000;Persist Security Info=True;database=SecureCookie&quot;\" />" );

                db.IndexSettings_Create( 1, 2, "<SolrSettings><Core url=\"http://10.211.55.9:8080/solr/core0\" type=\"1\" /><Core url=\"http://10.211.55.9:8080/solr/core1\" type=\"1\" /></SolrSettings>" );

				UserAnonymous     = db.UserInfo_Get( new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), null, null ).ToDTO().First();
				UserAdministrator = db.UserInfo_Get( new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), null, null ).ToDTO().First();

				AnonymousSession = db.Session_Get( new UUID( "12345678-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();
				AdminSession     = db.Session_Get( new UUID( "23456789-7D98-4F52-885E-AF4837FAA352" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();

				AdminGroup = db.Group_Get( new UUID( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ).ToByteArray(), null, null ).ToDTO().First();

				SubscriptionInfo = db.SubscriptionInfo_Get( new UUID("9C4E8A99-A69B-41FD-B1C7-E28C54D1D304").ToByteArray(), UserAdministrator.GUID.ToByteArray() ).ToDTO().First();
				ClientSettings = db.ClientSettings_Get( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray() ).ToDTO().First();
				UserSetting = db.UserSettings_Get( new UUID( "9EEE8A99-A69B-41FD-B1C7-E28C54D1D305" ).ToByteArray(), new UUID( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ).ToByteArray() ).ToDTO().First();
        	}

            PortalApplication = new PortalApplication( new MockCache(), new MockSolrManager() );

            AdminCallContext = new CallContext( PortalApplication, new PortalRequest(), new MockPortalResponse() );
            AnonCallContext  = new CallContext( PortalApplication, new PortalRequest(), new MockPortalResponse() );

            AdminCallContext.PortalRequest.Parameters.Add( "sessionGUID", "23456789-7D98-4F52-885E-AF4837FAA352" );
            AnonCallContext.PortalRequest.Parameters.Add( "sessionGUID", "12345678-7D98-4F52-885E-AF4837FAA352" );
        }
    }
}
