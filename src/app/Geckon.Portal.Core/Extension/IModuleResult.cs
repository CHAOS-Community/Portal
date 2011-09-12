using System.Collections.Generic;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public interface IModuleResult
    {
        IEnumerable<KeyValuePair<string, object>> Attributes { get; }
        IEnumerable<XmlSerialize>                 Elements { get; }

        void AddAttribute( KeyValuePair<string, object> nameValue );
        void AddElement( XmlSerialize value );

        void AddAttribute( params KeyValuePair<string, object>[] attributes);
    }
}
