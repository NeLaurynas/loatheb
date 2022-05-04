namespace Loatheb.steps.utilSteps;

public class TryResettingUIStep : StepBase
{
	public override ResettingUIStepState State
	{
		get;
	}

	public override void ResetState()
	{
		base.ResetState();
		State.NextStep = null;
	}

	public TryResettingUIStep()
	{
		State = new ResettingUIStepState();
	}

	public override async Task<StepBase?> Execute()
	{
		DI.Logger.Log("Trying to reset UI");
		DI.KbdCtrl.EscapeTwice();
		DI.MouseCtrl.SafePosition();
		Thread.Sleep(333);
		if (DI.OpenCV.IsMatching(DI.Images.GameMenu, 1150, 250, 400, 150, 0.9, true))
		{
			DI.Logger.Log("Game menu is showing, sending ESC");
			DI.KbdCtrl.Escape();
		}
		DI.Logger.Log("UI should be reset");

		return State.NextStep;
	}

	public override void AfterExec()
	{
		ResetState();
	}
}

public class ResettingUIStepState : StepStateBase
{
	public StepBase? NextStep { get; set; }
}
