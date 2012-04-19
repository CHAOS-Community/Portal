using CHAOS.Portal.Core.Standard;
using Geckon.Serialization;
using CHAOS.Portal.DTO.Standard;

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