namespace Loatheb.steps.grindSteps;

public class MoveUpDownStep : StepBase
{
	public override StepStateWithNextStep State
	{
		get;
	}

	public MoveUpDownStep()
	{
		State = new StepStateWithNextStep();
	}

	public override async Task<StepBase?> Execute()
	{
		await Task.Yield();
		var upCount = DI.Rnd.Next(150, 300);
		DI.MouseCtrl.MoveFromCenter(y: upCount);
		DI.MouseCtrl.Click();
		Thread.Sleep(1000);
		
		// always
		if (DI.Rnd.Next(100) < 1000)
		{
			DI.MouseCtrl.MoveFromCenter(y: -upCount + 30);
			DI.MouseCtrl.Click();
		}
		else
		{
			Thread.Sleep(100);
		}
		
		Thread.Sleep(DI.Cfg.DelayAfterUpAndDown);

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
