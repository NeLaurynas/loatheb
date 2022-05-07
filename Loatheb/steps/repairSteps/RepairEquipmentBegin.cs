namespace Loatheb.steps.repairSteps;

public class RepairEquipmentBegin : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public RepairEquipmentBegin()
	{
		State = new StepStateBase
		{
			SleepDurationBeforeExecuting = 1000
		};
	}

	public override async Task<StepBase?> Execute()
	{
		if (Utils.InsideChaosDungeon())
			return GrindSteps.LeaveChaosDungeonStep;

		Utils.TryUntilTrue(Utils.IsLoaded, 40, 1000);
		
		if (await NeedsRepairingEquipment())
			return RepairEquipmentSteps.OpenPetMenuStep;

		return GrindSteps.EnterChaosDungeonBegin;
	}
	
	public async Task<bool> NeedsRepairingEquipment()
	{
		DI.Logger.Log("Checking if gear needs repairing");
		var task1 = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.GearNeedsRepair1, 2000, 0, 150, 120));
		var task2 = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.GearNeedsRepair2, 2000, 0, 150, 120));
		var task3 = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.GearNeedsRepair3, 2000, 0, 150, 120));

		var res1 = await task1;
		var res2 = await task2;
		var res3 = await task3;

		return res1 || res2 || res3;
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
