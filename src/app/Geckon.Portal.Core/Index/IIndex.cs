using System;
using System.Collections.Generic;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Core.Index
{
    public interface IIndex
    {
        void Set( IEnumerable<IIndexable> items );
        void Set( IIndexable item );
        IEnumerable<IResult> Get( IQuery query );
    }
}
