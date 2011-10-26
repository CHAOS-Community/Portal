using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Module;

namespace Geckon.Portal.Core.Index
{
    public interface IIndexManager
    {
        IIndex GetIndex<T>() where T : IModule;
        IIndex GetIndex( string fullName );
        void AddIndex<T>( IIndexConnection connection ) where T : IModule;
        void AddIndex( string fullName, IIndexConnection connection );
    }
}
