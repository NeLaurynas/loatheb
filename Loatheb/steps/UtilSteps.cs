using Loatheb.steps.utilSteps;
namespace Loatheb.steps;

public static class UtilSteps
{
	private static TryResettingUIStep TryResettingUIStep { get; } = new();

	public static StepBase? CreateTryResettingUIStep(StepBase? nextStep = null)
	{
		TryResettingUIStep.State.NextStep = nextStep;
		return TryResettingUIStep;
	}
}
