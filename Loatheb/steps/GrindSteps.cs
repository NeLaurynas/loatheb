using Loatheb.steps.grindSteps;
namespace Loatheb.steps;

public static class GrindSteps
{
	public static EnterChaosDungeonBegin EnterChaosDungeonBegin { get; } = new();
	public static LeaveChaosDungeonStep LeaveChaosDungeonStep { get; } = new();
	public static ClickEnterChaosDungeonStep ClickEnterChaosDungeonStep { get; } = new();
	private static MoveUpDownStep MoveUpDownStep { get; } = new();
	public static P1MainBattleStep P1MainBattleStep { get; } = new();
	public static P2MainBattleStep P2MainBattleStep { get; } = new();
	public static ProceedToP2Step ProceedToP2Step { get; } = new();

	public static MoveUpDownStep? CreateMoveUpDownStep(StepBase? nextStep = null)
	{
		MoveUpDownStep.State.NextStep = nextStep;
		return MoveUpDownStep;
	}
}
