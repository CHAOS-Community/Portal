namespace Chaos.Portal.Protocol.Tests.v5.Module
{
    using System;
    using Core;
    using Core.Data;
    using Portal.Module;
    using Portal.v5.Extension;
    using Core.Extension;
    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalModuleTest
    {
        [Test]
        public void Load_NoErrors_MapAllExtensions()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();

            module.Load(application.Object);

            application.Verify(m => m.MapRoute("/v5/ClientSettings", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v5/Group", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v5/Subscription", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v5/User", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v5/UserSettings", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v5/View", It.IsAny<Func<IExtension>>()));
        }
    }
}