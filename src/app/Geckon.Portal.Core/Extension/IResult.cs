using System.Collections.Generic;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IResult
    {
        void Add( string moduleName, XmlSerialize obj, params KeyValuePair<string, object>[] attributes );
        void Add( string moduleName, IEnumerable<XmlSerialize> obj, params KeyValuePair<string, object>[] attributes );
        string Content { get; }
    }
}
