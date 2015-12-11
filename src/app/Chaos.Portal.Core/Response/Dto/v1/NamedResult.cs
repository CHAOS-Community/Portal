namespace Chaos.Portal.Core.Response.Dto.v1
{
    public class NamedResult
    {
        public string ModuleName { get; private set; }
        public object Obj { get; private set; }

        public NamedResult(string moduleName, object obj)
        {
            ModuleName = moduleName;
            Obj = obj;
        }
    }
}