using System;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    [Document("Geckon.Portal.Data.Dto.Module")]
    public class Module : XmlSerialize
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Configuration { get; set; }
        public DateTime DateCreated { get; set; }

        #endregion
        #region Construction

        public static Module Create( Data.Module module )
        {
            Module result = new Module();

            result.ID            = module.ID;
            result.Name          = module.Name;
            result.Path          = module.Path;
            result.Configuration = module.Configuration.ToString();
            result.DateCreated   = module.DateCreated;

            return result;
        }

        #endregion
    }
}
