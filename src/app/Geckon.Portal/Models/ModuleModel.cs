using System.Collections.Generic;
using Geckon.Portal.Data.Dto;

namespace Geckon.Portal.Models
{
    public class ModuleModel
    {
        #region Fields


        #endregion
        #region Properties

        private IList<Module> Modules { get; set; }

        #endregion
        #region Construction

        public ModuleModel( IEnumerable<Module> modules )
        {
            Modules = (IList<Module>) modules;
        }

        #endregion
    }
}
