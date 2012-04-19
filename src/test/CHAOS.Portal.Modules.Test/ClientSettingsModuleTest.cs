﻿using System.Configuration;
using CHAOS.Portal.Core.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class ClientSettingsModuleTest : TestBase
    {
        #region Constructors

        private ClientSettingsModule ClientSettingsModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            ClientSettingsModule = new ClientSettingsModule();
            ClientSettingsModule.Initialize( ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString );
        }

        #endregion

        [Test]
        public void Should_Get_ClientSettings()
        {
            var clientSettings = ClientSettingsModule.Get( AdminCallContext, ClientSettings.GUID );

            Assert.AreEqual( ClientSettings.GUID.ToByteArray(), clientSettings.GUID.ToByteArray() );
        }
    }
}
