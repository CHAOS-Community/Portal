﻿using System;
using System.Linq;
using Geckon.Portal.Data;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
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

        #endregion
        #region Constructions

        [SetUp]
        public void SetUp()
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                db.PopulateWithDefaultData();
                Session          = db.Session_Insert( Guid.NewGuid(), Guid.Parse( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ), 1 ).First();
                AdminSession     = db.Session_Insert( Guid.NewGuid(), Guid.Parse( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ), 1 ).First();
                User             = db.UserInfo_Get( Guid.Parse( "C0B231E9-7D98-4F52-885E-AF4837FAA352" ), null, null, null, null ).First();
                AdminUser        = db.UserInfo_Get( Guid.Parse( "A0B231E9-7D98-4F52-885E-AF4837FAA352" ), null, null, null, null ).First();
                AdminGroup       = db.Group_Get( null, Guid.Parse( "A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA" ), null, AdminUser.ID ).First();
                SubscriptionInfo = db.SubscriptionInfo_Get(null, Guid.Parse("9C4E8A99-A69B-41FD-B1C7-E28C54D1D304"), null, AdminUser.ID).First();
                UserSetting      = db.UserSettings_Get( AdminUser.ID, null, 1 ).First();
            }
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion
    }
}
