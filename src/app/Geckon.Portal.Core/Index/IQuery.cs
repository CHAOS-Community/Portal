using System;
using System.Collections.Generic;

namespace Geckon.Portal.Core.Index
{
    public interface IQuery
    {
        string Query { get; }
        string Sort { get; }

        /// <summary>
        /// This method will initialize the query. Reference the implementation documentation for information on Syntax and limitations
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sort"></param>
        void Init( string query, string sort );
    }
}
