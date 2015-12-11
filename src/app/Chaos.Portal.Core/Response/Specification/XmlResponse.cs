namespace Chaos.Portal.Core.Response.Specification
{
    using System.IO;
    using System.Xml.Linq;

    using CHAOS.Serialization.Standard;

    public class XmlResponse : IResponseSpecification
    {
        #region Initlization



        #endregion
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            var xdoc = SerializerFactory.Get<XDocument>().Serialize(response.Output, false);
            xdoc.Declaration = new XDeclaration("1.0", response.Encoding.HeaderName, "yes");

            var stream = new MemoryStream();

            xdoc.Save(stream, SaveOptions.DisableFormatting);

            stream.Position = 0;

            return stream;
        }

        #endregion
    }
}