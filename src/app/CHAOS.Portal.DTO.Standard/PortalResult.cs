using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace CHAOS.Portal.DTO.Standard
{
    [Serialize("PortalResult")]
    public class PortalResult : IPortalResult
    {
        #region Properties

        [Serialize("Duration")]
        [SerializeXML(true)]
        public long Duration
        {
            get{ return Timestamp.ElapsedMilliseconds; }
        }

        [Serialize("ModuleResults")]
        public IList<IModuleResult> Modules { get; set; }

        private Stopwatch Timestamp { get; set; }

        #endregion
        #region Construction

        public PortalResult() : this( new Stopwatch() )
        {
            
        }

        public PortalResult( Stopwatch timestamp )
        {
            Modules   = new List<IModuleResult>();
            Timestamp = timestamp;

            Timestamp.Start();
        }

        #endregion
        #region Business Logic

        public IModuleResult GetModule( string modulename )
        {
            IModuleResult result = Modules.FirstOrDefault( module => module.Fullname == modulename );

            if( result == null )
            {
                result = new ModuleResult( modulename );

                Modules.Add( result );
            }

            return result;
        }

        #endregion
    }
}
