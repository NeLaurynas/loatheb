namespace Loatheb.steps.repairSteps;

public class ClickRepairEquipmentStep : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public ClickRepairEquipmentStep()
	{
		State = new StepStateBase
		{
			SleepDurationBeforeExecuting = 333
		};
	}

	public override async Task<StepBase?> Execute()
	{
		await Task.Yield();
		var (matches, locations) = DI.OpenCV.IsMatchingWhere(DI.Images.RepairEquippedGearBtn, 1300, 350, 200, 150);

		if (matches)
		{
			DI.Logger.Log("Repairing gear");
			DI.MouseCtrl.MoveAndClick(locations);
			ResetState();
			UtilSteps.CreateTryResettingUIStep(GrindSteps.EnterChaosDungeonBegin);
		}
		
		DI.Logger.Log("Failed repairing gear, entering dungeon anyways");
		ResetState();
		return UtilSteps.CreateTryResettingUIStep(GrindSteps.EnterChaosDungeonBegin);
	}
}
