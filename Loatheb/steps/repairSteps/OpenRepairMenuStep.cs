namespace Loatheb.steps.repairSteps;

public class OpenRepairMenuStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		DI.Logger.Log("Opening repair window");

		var (matches, locations) = DI.OpenCV.IsMatchingWhere(DI.Images.RepairGearBtn, 1500, 550, 220, 200);

		if (matches)
		{
			DI.MouseCtrl.MoveAndClick(locations);
			ResetState();
			return RepairEquipmentSteps.ClickRepairEquipmentStep;
		}
			
		DI.Logger.Log("Pet function repair button not found, entering chaos dungeon anyways");

		ResetState();
		return UtilSteps.CreateTryResettingUIStep(GrindSteps.EnterChaosDungeonBegin);
	}
}
