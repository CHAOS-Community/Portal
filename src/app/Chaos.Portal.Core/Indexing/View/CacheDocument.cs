namespace Chaos.Portal.Core.Indexing.View
{
    public class CacheDocument
    {
        public string Id { get; set; }
        public object Dto { get; set; }

        public string Fullname
        {
            get { return Dto.GetType().FullName; }
        }
    }
}