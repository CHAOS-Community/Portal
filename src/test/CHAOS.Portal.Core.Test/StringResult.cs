using CHAOS.Portal.DTO.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Core.Test
{
    public class StringResult : Result
    {
        [Serialize]
        public string Result { get; set; }

        public StringResult( string result )
        {
            Result = result;
        }
    }
}