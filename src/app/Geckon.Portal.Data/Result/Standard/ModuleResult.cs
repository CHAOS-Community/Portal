using System.Collections.Generic;
using System.Diagnostics;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace Geckon.Portal.Data.Result.Standard
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
        public long Duration
        {
            get { return Timestamp.ElapsedMilliseconds; }
        }

        [Serialize("Count")]
        [SerializeXML(true)]
        public int Count
        {
            get { return Results.Count; }
        }

        [Serialize("Results")]
        public IList<IResult> Results { get; set; }

        private Stopwatch Timestamp { get; set; }

        #endregion
        #region Construction

        public ModuleResult( string fullname )
        {
            Timestamp = new Stopwatch();
            Timestamp.Start();

            Fullname = fullname;
            Results  = new List<IResult>();
        }

        #endregion
        #region Business Logic

        public void AddResult( IEnumerable<IResult> results )
        {
            foreach( IResult result in results )
            {
                AddResult( result );
            }
        }

        public void AddResult( IResult result )
        {
            Results.Add( result );
        }

        #endregion
    }
}
