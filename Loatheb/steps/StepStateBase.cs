namespace Loatheb.steps;

public class StepStateBase
{
	public int MaxIter { get; set; } = 1;

	public int Iter { get; set; } = 0;

	public int? SleepDurationBeforeExecuting { get; set; } = null;

	public int? SleepDurationAfterExecution { get; set; } = null;
}

public class StepStateWithNextStep : StepStateBase
{
	public StepBase? NextStep { get; set; }
}
