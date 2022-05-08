using Loatheb.steps.grindSteps;
namespace Loatheb.steps.utilSteps;

public class LeaveFromDeadWindowStep : StepBase
{
	public override StepStateWithNextStep State
	{
		get;
	}

	public LeaveFromDeadWindowStep()
	{
		State = new StepStateWithNextStep();
	}

	public override void ResetState()
	{
		base.ResetState();
		State.NextStep = null;
	}

	public override void AfterExec()
	{
		base.AfterExec();
		ResetState();
	}

	public override async Task<StepBase?> Execute()
	{
		DI.Logger.Log("Checking if leave button is visible");
		await Task.Yield();
		Thread.Sleep(7000);
		var (matching, location) = DI.OpenCV.IsMatchingWhere(DI.Images.LeaveDiedBtn, 1200, 0, 1200, 800, 0.7);

		if (matching)
		{
			DI.Logger.Log("Found, clicking");
			DI.MouseCtrl.MoveAndClick(location);
			Thread.Sleep(1500);
			Utils.ClickOkCenter();
		}
		else
			DI.Logger.Log("Leave button not found :(");

		return State.NextStep;
	}
}
