using System;
using System.Collections.Generic;

namespace Geckon.Portal.Core.Index
{
    public interface IIndexable
    {
        IEnumerable<KeyValuePair<string, string>> GetIndexableFields();
    }
}
