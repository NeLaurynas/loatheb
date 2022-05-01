namespace Loatheb.steps;

public class StepState
{
	public int IterCount { get; set; }

	public int MaxIter { get; set; } = 1;
	
	public int? SleepDurationBeforeExecuting { get; set; }

	public DateTime StartTime { get; set; }
}
