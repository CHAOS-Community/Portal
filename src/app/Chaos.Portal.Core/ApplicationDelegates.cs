namespace Chaos.Portal.Core
{
	using System;
    using Module;
	using Response;

    public class ApplicationDelegates
    {
        public delegate void ModuleHandler(object sender, ModuleArgs args);

        public class ModuleArgs : EventArgs
        {
            public IBaseModule Module { get; set; }

            public ModuleArgs(IBaseModule module)
            {
                Module = module;
            }
        }
    }
}
