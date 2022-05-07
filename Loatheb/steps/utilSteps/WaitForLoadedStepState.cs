namespace Loatheb.steps.utilSteps;

public class WaitForLoadedStep : StepBase
{
	public override WaitForLoadedStepState State
	{
		get;
	}

	public WaitForLoadedStep()
	{
		State = new WaitForLoadedStepState()
		{
			MaxIter = 1,
			SleepDurationBeforeExecuting = 1000
		};
	}

	public override async Task<StepBase?> Execute()
	{
		await Task.Yield();
		Utils.TryUntilTrue(Utils.IsLoaded, 40, 1000);

		return State.NextStep;
	}

	public override void AfterExec()
	{
		ResetState();
	}

	public override void ResetState()
	{
		base.ResetState();
		State.NextStep = null;
	}
}

public class WaitForLoadedStepState : StepStateBase
{
	public StepBase? NextStep { get; set; }
}
