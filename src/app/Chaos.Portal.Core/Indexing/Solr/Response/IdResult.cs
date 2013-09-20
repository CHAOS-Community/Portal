namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Linq;
    using System.Xml.Linq;

    public class IdResult : IIndexResult
    {
        #region Properties

        public string Id { get; private set; }

        public float Score { get; set; }

        #endregion
        #region Business Logic

        public IIndexResult Init(XElement element)
        {
            Id = element.Elements("str").First(node => node.Attribute("name").Value == "Id").Value;

            var scoreElement = element.Elements("float").FirstOrDefault(item =>
                {
                    var xAttribute = item.Attribute("score");

                    return xAttribute != null && xAttribute.Value == "score";
                });

            if (scoreElement != null) Score = float.Parse(scoreElement.Value);

            return this;
        }

        #endregion
    }
}