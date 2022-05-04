namespace Loatheb.steps;

public abstract class StepBase
{
	public StepBase()
	{
		State = new StepStateBase();
	}
	
	public virtual StepStateBase State { get; }

	public abstract Task<StepBase?> Execute();

	public virtual void ResetState()
	{
		State.Iter = 0;
	}
	
	public virtual void AfterExec() {}
}
