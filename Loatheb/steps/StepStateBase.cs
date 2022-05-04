namespace Loatheb.steps;

public class StepStateBase
{
	public int MaxIter { get; set; } = 1;

	public int Iter { get; set; } = 0;

	public int? SleepDurationBeforeExecuting { get; set; } = null;

	public static StepStateBase Default = new();
}
