namespace Chaos.Portal.Module
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;
    using CHAOS;
    using Core;
    using Core.Bindings.Standard;
    using Core.Indexing;
    using Core.Module;

    public class PortalModule : IModuleConfig
    {
        public void Load(IPortalApplication portalApplication)
        {
            portalApplication.AddBinding(typeof(string), new StringParameterBinding());
            portalApplication.AddBinding(typeof(long), new ConvertableParameterBinding<long>());
            portalApplication.AddBinding(typeof(int), new ConvertableParameterBinding<int>());
            portalApplication.AddBinding(typeof(short), new ConvertableParameterBinding<short>());
            portalApplication.AddBinding(typeof(ulong), new ConvertableParameterBinding<ulong>());
            portalApplication.AddBinding(typeof(uint), new ConvertableParameterBinding<uint>());
            portalApplication.AddBinding(typeof(ushort), new ConvertableParameterBinding<ushort>());
            portalApplication.AddBinding(typeof(double), new ConvertableParameterBinding<double>());
            portalApplication.AddBinding(typeof(float), new ConvertableParameterBinding<float>());
            portalApplication.AddBinding(typeof(bool), new ConvertableParameterBinding<bool>());
            portalApplication.AddBinding(typeof(DateTime), new DateTimeParameterBinding());
            portalApplication.AddBinding(typeof(long?), new ConvertableParameterBinding<long>());
            portalApplication.AddBinding(typeof(int?), new ConvertableParameterBinding<int>());
            portalApplication.AddBinding(typeof(short?), new ConvertableParameterBinding<short>());
            portalApplication.AddBinding(typeof(ulong?), new ConvertableParameterBinding<ulong>());
            portalApplication.AddBinding(typeof(uint?), new ConvertableParameterBinding<uint>());
            portalApplication.AddBinding(typeof(ushort?), new ConvertableParameterBinding<ushort>());
            portalApplication.AddBinding(typeof(double?), new ConvertableParameterBinding<double>());
            portalApplication.AddBinding(typeof(float?), new ConvertableParameterBinding<float>());
            portalApplication.AddBinding(typeof(bool?), new ConvertableParameterBinding<bool>());
            portalApplication.AddBinding(typeof(DateTime?), new DateTimeParameterBinding());
            portalApplication.AddBinding(typeof(Guid), new GuidParameterBinding());
            portalApplication.AddBinding(typeof(Guid?), new GuidParameterBinding());
            portalApplication.AddBinding(typeof(IQuery), new QueryParameterBinding());
            portalApplication.AddBinding(typeof(IEnumerable<Guid>), new EnumerableOfGuidParameterBinding());
            portalApplication.AddBinding(typeof(IEnumerable<string>), new EnumerableOfStringParameterBinding());
            portalApplication.AddBinding(typeof(XDocument), new XDocumentBinding());
            portalApplication.AddBinding(typeof(UUID), new UUIDParameterBinding());

            portalApplication.MapRoute("/v5/ClientSettings", () => new v5.Extension.ClientSettings(portalApplication));
            portalApplication.MapRoute("/v5/Group", () => new v5.Extension.Group(portalApplication));
            portalApplication.MapRoute("/v5/Session", () => new v5.Extension.Session(portalApplication));
            portalApplication.MapRoute("/v5/Subscription", () => new v5.Extension.Subscription(portalApplication));
            portalApplication.MapRoute("/v5/User", () => new v5.Extension.User(portalApplication));
            portalApplication.MapRoute("/v5/UserSettings", () => new v5.Extension.UserSettings(portalApplication));
            portalApplication.MapRoute("/v5/View", () => new v5.Extension.View(portalApplication));

            portalApplication.MapRoute("/v6/ClientSettings", () => new v6.Extension.ClientSettings(portalApplication));
            portalApplication.MapRoute("/v6/Group", () => new v6.Extension.Group(portalApplication));
            portalApplication.MapRoute("/v6/Session", () => new v6.Extension.Session(portalApplication));
            portalApplication.MapRoute("/v6/Subscription", () => new v6.Extension.Subscription(portalApplication));
            portalApplication.MapRoute("/v6/User", () => new v6.Extension.User(portalApplication));
            portalApplication.MapRoute("/v6/UserSettings", () => new v6.Extension.UserSettings(portalApplication));
            portalApplication.MapRoute("/v6/View", () => new v5.Extension.View(portalApplication));
        }
    }
}