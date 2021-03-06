namespace Chaos.Portal.Core.Data.Model
{
    using CHAOS.Serialization;

    public class ScalarResult : AResult
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
