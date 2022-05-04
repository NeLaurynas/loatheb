namespace Loatheb.steps.grindSteps;

public class ClickEnterChaosDungeonStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		var dungeonSelected = false;

		for (var i = 0; i < 2; i++)
		{
			DI.Logger.Log("Chaos Dungeon window is showing");
			if (!DungeonSelected())
			{
				var (dungeonCanBeSelected, location) = DungeonNotSelected();
				if (dungeonCanBeSelected)
				{
					DI.MouseCtrl.MoveAndClick(location);
					Thread.Sleep(200);
				}
			}
			else
			{
				dungeonSelected = true;
				DI.Logger.Log("Dungeon is selected");
				break;
			}
		}

		if (dungeonSelected)
		{
			if (ClickEnter())
			{
				var clickedAccept = false;
				
				for (var i = 0; i < 10; i++)
				{
					Thread.Sleep(200);
					if (ClickAccept())
					{
						clickedAccept = true;
						DI.Logger.Log("Clicked Accept, should begin loading dungeon!");
						Thread.Sleep(1500);
						break;
					}
				}

				if (!clickedAccept)
				{
					DI.Logger.Log("Couldn't click Accept");
					return null;
				}
			}
			else
			{
				DI.Logger.Log("Couldn't click Enter");
				return null;
			}
		}
		
		Utils.TryUntilTrue(Utils.IsLoaded, 40, 1000);

		if (Utils.TryUntilTrue(Utils.InsideChaosDungeon, 5, 1000))
		{
			return GrindSteps.P1MoveUpDownStep;
		}

		return RepairEquipmentSteps.RepairEquipmentBegin;
	}

	public bool ClickEnter()
	{
		Console.WriteLine("Trying to click Enter");
		var (ok, location) = DI.OpenCV.IsMatchingWhere(DI.Images.EnterBtn, 0.9, ScreenPart.Right);
		if (!ok) return false;

		DI.MouseCtrl.MoveAndClick(location);
		return true;
	}

	public bool ClickAccept()
	{
		Console.WriteLine("Trying to click Accept");
		var (ok, location) = DI.OpenCV.IsMatchingWhere(DI.Images.AcceptBtn);
		if (!ok) return false;

		DI.MouseCtrl.MoveAndClick(location);
		return true;
	}

	public bool DungeonSelected()
	{
		DI.Logger.Log("Checking if chaos dungeon is selected");
		return DI.OpenCV.IsMatching(DI.Images.DungeonSelected, 0.95, ScreenPart.Left);
	}

	public (bool ok, Point[] locations) DungeonNotSelected()
	{
		DI.Logger.Log("Checking if chaos dungeon can be selected");
		return DI.OpenCV.IsMatchingWhere(DI.Images.DungeonNotSelected, 0.95, ScreenPart.Left);
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
