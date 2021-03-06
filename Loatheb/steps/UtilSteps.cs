using Loatheb.steps.utilSteps;
namespace Loatheb.steps;

public static class UtilSteps
{
	private static TryResettingUIStep TryResettingUIStep { get; } = new();
	private static WaitForLoadedStep WaitForLoadedStep { get; } = new();
	private static LeaveFromDeadWindowStep LeaveFromDeadWindowStep { get; } = new();

	public static StepBase CreateTryResettingUIStep(StepBase? nextStep = null)
	{
		TryResettingUIStep.State.NextStep = nextStep;
		return TryResettingUIStep;
	}

	public static StepBase CreateWaitForLoadedStep(StepBase? nextStep = null)
	{
		WaitForLoadedStep.State.NextStep = nextStep;
		return WaitForLoadedStep;
	}

	public static StepBase CreateLeaveFromDeadWindowStep(StepBase? nextStep = null)
	{
		LeaveFromDeadWindowStep.State.NextStep = nextStep;
		return LeaveFromDeadWindowStep;
	}
}
