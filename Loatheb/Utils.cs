namespace Loatheb;

public static class Utils
{
	public static void ActivateLAWindow()
	{
		DI.MouseCtrl.Move(DI.Sys.LAScreenX + 1, DI.Sys.LAScreenY + 1);
		DI.MouseCtrl.Click();
	}

	public static bool ClickOkCenter()
	{
		DI.Logger.Log("Trying to click OK button in center");
		var (matches, locations) = DI.OpenCV.IsMatchingWhere(DI.Images.OkBtn, 1050, 420, 500, 300);

		if (!matches) return false;

		DI.MouseCtrl.MoveAndClick(locations);
		return true;
	}

	public static bool TryUntilTrue(Func<bool> action, int retryTime = 5, int sleepTime = 100)
	{
		for (var i = 0; i < retryTime; i++)
		{
			Thread.Sleep(sleepTime);
			var result = action();
			if (result)
				return true;
		}

		return false;
	}
	
	public static bool InsideChaosDungeon()
	{
		DI.Logger.Log("Checking if inside chaos dungeon");
		return DI.OpenCV.IsMatching(DI.Images.LeaveBtn, 5, 30, 280, 300);
	}

	public static bool IsLoaded()
	{
		DI.Logger.Log("Checking if in loading screen");
		Thread.Sleep(1000);
		return DI.OpenCV.IsMatching(DI.Images.TopRightElement, 2525, 0, 40, 40, 0.95);
	}
}
