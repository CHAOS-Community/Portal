using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CHAOS.Portal.DTO;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace CHAOS.Portal.Core.Standard
{
    [Serialize("ModuleResult")]
    public class ModuleResult : IModuleResult
    {
        #region Properties

        [Serialize("Fullname")]
        [SerializeXML(true)]
        public string Fullname { get; set; }

        [Serialize("Duration")]
        [SerializeXML(true)]
        public long Duration { get; private set; }

        [Serialize("Count")]
        [SerializeXML(true)]
        public int Count
        {
            get { return Results.Count; }
        }

        [Serialize("TotalCount")]
        [SerializeXML(true)]
        public int? TotalCount
        {
            get;
            set;
        }

        [Serialize("PageIndex")]
        [SerializeXML(true)]
        public int? PageIndex
        {
            get;
            set;
        }

        [Serialize("TotalPages")]
        [SerializeXML(true)]
        public int? TotalPages
        {
            get;
            set;
        }

        [Serialize("Results")]
        public IList<IResult> Results { get; set; }

        private Stopwatch Timestamp { get; set; }


        #endregion
        #region Construction

        public ModuleResult( string fullname ) : this( fullname, new List<IResult>(), null, null, null )
        {
        }

        public ModuleResult( string fullname, IEnumerable<IResult> results, int? pageIndex, int? totalPages, int? totalCount )
        {
            Timestamp = new Stopwatch();
            Timestamp.Start();

            Fullname   = fullname;
            Results    = results.ToList();
            TotalPages = totalPages;
            PageIndex  = pageIndex;
            TotalCount = totalCount;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Adds an IEnumerable<IResult>.
        /// </summary>
        /// <param name="results"></param>
        public void AddResult( IEnumerable<IResult> results )
        {
            foreach( IResult result in results )
            {
                AddResult( result );
            }

            Duration = Timestamp.ElapsedMilliseconds;
        }

        /// <summary>
        /// Adds a single result. If you need to add a list of results use AddResult( IEnumerable<IResult> results ) for better performance
        /// </summary>
        /// <param name="result"></param>
        public void AddResult( IResult result )
        {
            Results.Add( result );

            Duration = Timestamp.ElapsedMilliseconds;
        }

        #endregion
    }
}
