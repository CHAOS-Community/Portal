namespace Chaos.Portal.Data.Dto
{
    using CHAOS.Serialization;

    public class ScalarAResult : AResult
	{
		#region Properties

		[Serialize("Value")]
		public int Value { get; set; }

		#endregion
		#region Constructors

		public ScalarAResult(int value)
		{
			Value = value;
		}

		public ScalarAResult()
		{
		}

		#endregion
	}
}
