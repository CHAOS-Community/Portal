namespace Chaos.Portal.Core.Data
{
    using System;
    using System.Linq;
    using CHAOS.Data;
    using CHAOS.Data.MySql;
    using Exceptions;
    using Mappings;
    using MySql.Data.MySqlClient;

    public interface IModuleRepository
    {
        Model.Module Get(string name);
        void Set(Model.Module module);
    }

    public class ModuleRepository : IModuleRepository
    {
        private Gateway Gateway { get; set; }

        public ModuleRepository(Gateway gateway)
        {
            Gateway = gateway;
        }

        static ModuleRepository()
        {
            ReaderExtensions.Mappings.Add(typeof(Model.Module), new ModuleMapping());
        }

        public Model.Module Get(string name)
        {
            var module = Gateway.ExecuteQuery<Model.Module>("Module_Get", new[]
                {
                    new MySqlParameter("ID", null), 
                    new MySqlParameter("Name", name)
                }).FirstOrDefault();

            if(module == null)
                throw new ArgumentException("Module not found", name);

            return module;
        }

        public void Set(Model.Module module)
        {
            var result = Gateway.ExecuteNonQuery("Module_Set", new[]
                {
                    new MySqlParameter("Id", module.ID),
                    new MySqlParameter("Name", module.Name),
                    new MySqlParameter("Configuration", module.Configuration)
                });

            if(result == -666) throw new NotImplementedException("Updating Modules are not implemented");
            if(result <= 0) throw new UnhandledException("Module/Set failed on the database");
        }
    }
}