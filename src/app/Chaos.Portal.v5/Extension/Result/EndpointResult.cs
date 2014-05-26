namespace Chaos.Portal.v5.Extension.Result
{
    using CHAOS.Serialization;
    using Core.Data.Model;

    public class EndpointResult : AResult
    {
        [Serialize]
        public bool WasSuccess { get; set; }

        public static EndpointResult Success()
        {
            return new EndpointResult { WasSuccess = true };
        }

        public static EndpointResult Failed()
        {
            return new EndpointResult { WasSuccess = false };
        }
    }
}