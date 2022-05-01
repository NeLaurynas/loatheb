namespace Loatheb.steps;

public class Step
{
	internal readonly Logger _logger;
	
	public StepState State { get; }
	
	public Step()
	{
		_logger = DI.Logger;
		State = new StepState
		{
			StartTime = DateTime.Now
		};
	}

	public virtual async Task<Step?> Execute()
	{
		await Task.Yield();
		throw new NotImplementedException();
	}
}
