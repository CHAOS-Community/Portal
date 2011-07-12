using System.Collections.Generic;
using Geckon.Data;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IResult
    {
        void Add( string moduleName, XmlSerialize obj, params NameValue[] attributes );
        void Add( string moduleName, IEnumerable<XmlSerialize> obj, params NameValue[] attributes );
        string Content { get; }
    }
}
