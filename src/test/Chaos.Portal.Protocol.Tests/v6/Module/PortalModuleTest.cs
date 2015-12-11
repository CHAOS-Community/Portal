namespace Chaos.Portal.Protocol.Tests.v6.Module
{
    using System;

    using Core;
    using Portal.Module;
    using Core.Extension;
    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalModuleTest
    {
        [Test]
        public void Load_NoErrors_MapAllExtensions()
        {
            var module = new PortalModule();
            var application = new Mock<IPortalApplication>();

            module.Load(application.Object);

            application.Verify(m => m.MapRoute("/v6/ClientSettings", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v6/Group", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v6/Subscription", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v6/User", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v6/UserSettings", It.IsAny<Func<IExtension>>()));
            application.Verify(m => m.MapRoute("/v6/View", It.IsAny<Func<IExtension>>()));
        }
    }
}