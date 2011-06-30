using System.Collections.Generic;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IResult
    {
        void Add( XmlSerialize obj );
        void Add( IEnumerable<XmlSerialize> obj );
        IEnumerable<XmlSerialize> Content { get; }
    }
}
