using CHAOS.Serialization;

namespace CHAOS.Portal.DTO.Standard
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
