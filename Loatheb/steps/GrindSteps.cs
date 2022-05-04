using Loatheb.steps.grindSteps;
namespace Loatheb.steps;

public static class GrindSteps
{
	public static EnterChaosDungeonBegin EnterChaosDungeonBegin => new();
	public static LeaveChaosDungeonStep LeaveChaosDungeonStep => new();
	public static ClickEnterChaosDungeonStep ClickEnterChaosDungeonStep => new();
	public static P1MoveUpDownStep P1MoveUpDownStep => new();
	public static P1MainBattleStep P1MainBattleStep => new();
}
