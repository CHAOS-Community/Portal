using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class ScalarResult : Result
	{
		#region Properties

		[Serialize("Value")]
		public int Value { get; set; }

		#endregion
		#region Constructors

		public ScalarResult(int value)
		{
			Value = value;
		}

		public ScalarResult()
		{
		}

		#endregion
	}
}
