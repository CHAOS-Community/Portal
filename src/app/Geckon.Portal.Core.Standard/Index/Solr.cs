using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard
{
    public class Solr : IIndex
    {
        #region Properties



        #endregion
        #region Construction

        public Solr()
        {

        }

        #endregion
        #region Business Logic

        public void Set( IEnumerable<IIndexable> items )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Result.IResult> Get( IQuery query )
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
