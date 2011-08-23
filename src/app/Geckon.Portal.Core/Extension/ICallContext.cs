using System.Collections.Generic;
using Geckon.Portal.Data;

namespace Geckon.Portal.Core.Extension
{
    public interface ICallContext
    {
        string SessionID { get; set; }
        UserInfo User { get; }
        ICache Cache { get; set; }
        ISolr Solr { get; set; }
        IEnumerable<Parameter> Parameters { get; set; }
    }
}
