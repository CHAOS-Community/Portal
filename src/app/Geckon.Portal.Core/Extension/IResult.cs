using System.Collections.Generic;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IResult
    {
        void Add( string moduleName, XmlSerialize obj );
        void Add(string moduleName, IEnumerable<XmlSerialize> obj);
        string Content { get; }
    }
}
