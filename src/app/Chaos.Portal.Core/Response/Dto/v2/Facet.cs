namespace Chaos.Portal.Core.Response.Dto.v2
{
    using CHAOS.Serialization;

    public class Facet
    {
        #region Initialization

        public Facet(string key, uint count)
        {
            Key   = key;
            Count = count;
        }

        #endregion
        #region Properties

        [Serialize]
        public string Key { get; set; }

        [Serialize]
        public uint Count { get; set; }

        #endregion
    }
}
