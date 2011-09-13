using System.Collections.Generic;
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
        public double Duration
        {
            get { return 0.0; }
        }

        [Serialize("Count")]
        [SerializeXML(true)]
        public int Count
        {
            get { return Results.Count; }
        }

        [Serialize("Results")]
        public IList<IResult> Results { get; set; }

        #endregion
        #region Construction

        public ModuleResult( string fullname )
        {
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
