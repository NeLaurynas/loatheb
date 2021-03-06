namespace Loatheb.steps.grindSteps;

public class LeaveChaosDungeonStep : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public LeaveChaosDungeonStep()
	{
		State = new StepStateBase
		{
			MaxIter = 4,
			SleepDurationBeforeExecuting = 200
		};
	}

	public override async Task<StepBase?> Execute()
	{
		await Task.Yield();
		var (matches, locations) = DI.OpenCV.IsMatchingWhere(DI.Images.LeaveBtn, 5, 30, 280, 300);
		if (matches)
		{
			DI.MouseCtrl.MoveAndClick(locations);
			Thread.Sleep(300);
			if (Utils.TryUntilTrue(Utils.ClickOkCenter))
			{
				ResetState();
				return RepairEquipmentSteps.RepairEquipmentBegin;
			}
		}

		Utils.TryUntilTrue(Utils.IsLoaded, 30, 1000);

		if (Utils.InsideChaosDungeon())
		{
			return this;
		}
		
		ResetState();
		return RepairEquipmentSteps.RepairEquipmentBegin;
	}
}
