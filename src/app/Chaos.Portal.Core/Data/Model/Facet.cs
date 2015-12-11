namespace Chaos.Portal.Core.Data.Model
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
