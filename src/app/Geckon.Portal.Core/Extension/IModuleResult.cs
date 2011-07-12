using System.Collections.Generic;
using Geckon.Data;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IModuleResult
    {
        IEnumerable<NameValue>    Attributes { get; }
        IEnumerable<XmlSerialize> Elements { get; }

        void AddAttribute( NameValue nameValue );
        void AddElement( XmlSerialize value );

        void AddAttribute(NameValue[] attributes);
    }
}
