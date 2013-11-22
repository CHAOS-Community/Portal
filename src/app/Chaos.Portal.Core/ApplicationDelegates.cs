namespace Chaos.Portal.Core
{
    using Core.Module;

    public class ApplicationDelegates
    {
        public delegate void ModuleHandler(object sender, ModuleArgs args);

        public class ModuleArgs
        {
            public IModule Module { get; set; }

            public ModuleArgs(IModule module)
            {
                Module = module;
            }
        }
    }
}
