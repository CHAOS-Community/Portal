namespace Chaos.Portal.Core
{
	using System;
    using Module;

    public class ApplicationDelegates
    {
        public delegate void ModuleHandler(object sender, ModuleArgs args);

        public class ModuleArgs : EventArgs
        {
            public IModule Module { get; set; }

            public ModuleArgs(IModule module)
            {
                Module = module;
            }
        }
    }
}
