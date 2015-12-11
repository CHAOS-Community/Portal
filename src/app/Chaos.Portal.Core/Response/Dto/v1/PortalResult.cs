namespace Chaos.Portal.Core.Response.Dto.v1
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    [Serialize("PortalResult")]
    public class PortalResult : IPortalResult
    {
        #region Properties

        [Serialize("Duration")]
        [SerializeXML(true)]
        public long Duration
        {
            get { return Timestamp.ElapsedMilliseconds; }
        }

        [Serialize("ModuleResults")]
        public IList<IModuleResult> Modules { get; set; }

        private Stopwatch Timestamp { get; set; }

        #endregion
        #region Construction

        public PortalResult()
            : this(new Stopwatch())
        {

        }

        public PortalResult(Stopwatch timestamp)
        {
            Modules   = new List<IModuleResult>();
            Timestamp = timestamp;
        }

        #endregion
        #region Business Logic

        public IModuleResult GetModule(string modulename)
        {
            var result = Modules.FirstOrDefault(module => module.Fullname == modulename);

            if (result == null)
            {
                result = new ModuleResult(modulename);

                Modules.Add(result);
            }

            return result;
        }

        #endregion
    }
}