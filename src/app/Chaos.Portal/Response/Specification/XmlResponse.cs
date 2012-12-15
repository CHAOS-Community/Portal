using System.IO;
using System.Xml.Linq;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class XmlResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            var xdoc = SerializerFactory.Get<XDocument>().Serialize(response, false);
            xdoc.Declaration = new XDeclaration("1.0", "UTF-8", "yes");

            var stream = new MemoryStream();

            xdoc.Save(stream);

            return stream;
        }

        #endregion
    }
}