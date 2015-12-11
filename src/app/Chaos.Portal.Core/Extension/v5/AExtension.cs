using Chaos.Portal.Core.Response.Dto.v1;

namespace Chaos.Portal.Core.Extension.v5
{
    public abstract class AExtension : Extension.AExtension
    {
        public string ModuleName { get; set; }

        protected AExtension(IPortalApplication portalApplication, string moduleName) : base(portalApplication)
        {
            ModuleName = moduleName;
        }

        protected override void WriteToOutput(object result)
        {
            var namedResult = new NamedResult(ModuleName, result);

            base.WriteToOutput(namedResult);
        }
    }
}
