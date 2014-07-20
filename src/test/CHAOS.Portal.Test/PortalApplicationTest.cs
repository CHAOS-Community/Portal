namespace Chaos.Portal.Test
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Cache;
    using Core.Data;
    using Core.Data.Model;
    using Core.Exceptions;
    using Core.Indexing;
    using Core.Indexing.Solr;
    using Core.Indexing.Solr.Response;
    using Core.Indexing.View;
    using Core.Module;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest
    {
        public Mock<ICache> Cache { get; private set; }
        public Mock<IViewManager> Index { get; private set; }
        public Mock<IPortalRepository> Repo { get; private set; }

        [SetUp]
        public void Setup()
        {
            Cache = new Mock<ICache>();
            Index = new Mock<IViewManager>();
            Repo = new Mock<IPortalRepository>();
        }

        [Test]
        public void GetSettings_SettingsInXml_GetFromDatabaseAndTryParse()
        {
            var app = Make_PortalApplication();
            var key = "valid key";
            Repo.Setup(m => m.Module.Get(key)).Returns(new Module { Configuration = "<TestSettings><Val1>1</Val1></TestSettings>" });

            var result = app.GetSettings<TestSettings>(key);

            Assert.That(result.IsValid(), Is.True);
            Assert.That(result.Val1, Is.EqualTo("1"));
            Repo.VerifyAll();
        }

        [Test]
        public void GetSettings_SettingsInJson_GetFromDatabaseAndTryParse()
        {
            var app = Make_PortalApplication();
            var key = "valid key";
            Repo.Setup(m => m.Module.Get(key)).Returns(new Module { Configuration = "{\"Val1\":\"1\"}" });

            var result = app.GetSettings<TestSettings>(key);

            Assert.That(result.IsValid(), Is.True);
            Assert.That(result.Val1, Is.EqualTo("1"));
            Repo.VerifyAll();
        }

        [Test, ExpectedException(typeof(ModuleConfigurationMissingException))]
        public void GetSettings_SettingsAreInvalid_GetFromDatabaseAndTryParse()
        {
            var app = Make_PortalApplication();
            var key = "valid key";
            Repo.Setup(m => m.Module.Get(key)).Returns(new Module { Configuration = "{\"Val1\":null}" });

            app.GetSettings<TestSettings>(key);
        }

        [Test, ExpectedException(typeof(ModuleConfigurationMissingException))]
        public void GetSettings_SettingsNotFound_CreateTemplateInDatabase()
        {
            var app = Make_PortalApplication();
            var key = "valid key";
            Repo.Setup(m => m.Module.Get(key)).Throws<ArgumentException>();

            app.GetSettings<TestSettings>(key);

            Repo.Verify(m => m.Module.Set(It.IsAny<Module>()));
        }

//        [Test]
//        public void AddView_MockView_CanBeRetrievedFromTheViewManager()
//        {
//            var app = new PortalApplication(Cache.Object, new ViewManager(Cache.Object), Repo.Object, null );
//            var view = new Mock<IView>();
//
//            app.AddView("MyView", () => view.Object);
//          
//            var actual = app.ViewManager.GetView("MyView");
//            Assert.That(actual, Is.Not.Null);
//        }

        private PortalApplication Make_PortalApplication()
        {
            return new PortalApplication(Cache.Object, Index.Object, Repo.Object, null);
        }

        private class TestSettings : IModuleSettings
        {
            public string Val1 { get; set; }

            public bool IsValid()
            {
                return !string.IsNullOrEmpty(Val1);
            }
        }
    }
}
