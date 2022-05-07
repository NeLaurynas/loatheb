using Loatheb.win32;
namespace Loatheb.steps.grindSteps;

public class ProceedToP2Step : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public ProceedToP2Step()
	{
		State = new StepStateBase
		{
			SleepDurationBeforeExecuting = 100,
			SleepDurationAfterExecution = 2000,
			MaxIter = 2
		};
	}

	public override async Task<StepBase?> Execute()
	{
		DI.Logger.Log("Checking for enter portal icon");
		if (DI.OpenCV.IsMatching(DI.Images.MoveIcon, ScreenLocations.MiddleOfTheScreen, 0.85d))
		{
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			return TryAfterEnter();
		}
		
		DI.Logger.Log("Checking for blue portal");
		var bluePortal1Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal1, 0.7d)); 
		var bluePortal2Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal2, 0.7d)); 
		var bluePortal3Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal3, 0.7d)); 
		var bluePortal4Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal4, 0.7d));

		var tasks = await Task.WhenAll(bluePortal1Task, bluePortal2Task, bluePortal3Task, bluePortal4Task);

		var matchesPortal = tasks.FirstOrDefault(x => x.isMatching);
		if (matchesPortal.isMatching)
		{
			DI.Logger.Log("Found, moving to it");
			DI.MouseCtrl.MoveAndClick(matchesPortal.maxLocations);
			return TryAfterEnter();
		}
		
		// DI.Logger.Log("Checking for blue portal minimap");
		// var blueMini1Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini1, ScreenLocations.Minimap, 0.8d));
		// var blueMini2Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini2, ScreenLocations.Minimap, 0.8d));
		// var blueMini3Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini3, ScreenLocations.Minimap, 0.8d));
		// var blueMini4Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini4, ScreenLocations.Minimap, 0.8d));
		//
		// var tasksMini = await Task.WhenAll(blueMini1Task, blueMini2Task, blueMini3Task, blueMini4Task);
		//
		// var matchesMini = tasksMini.FirstOrDefault(x => x.isMatching);
		// if (matchesMini.isMatching)
		// {
		// 	DI.Logger.Log("Found minimap, moving to it");
		// 	DI.MouseCtrl.MoveMinimapDistance(matchesMini.maxLocations);
		// 	DI.Logger.Log("TODO: CHECK IF ENTERED?");
		// 	return TryAfterEnter();
		// }
		
		DI.MouseCtrl.MoveFromCenter(0, 300);
		
		if (DI.OpenCV.IsMatching(DI.Images.MoveIcon, ScreenLocations.MiddleOfTheScreen, 0.85d))
		{
			DI.Logger.Log("Found enter icon, sending G");
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			return TryAfterEnter();
		}

		return this;
	}

	private StepBase? TryAfterEnter()
	{
		Thread.Sleep(1500);
		// so chaining with extension methods would be nice eh?
		return UtilSteps.CreateWaitForLoadedStep(GrindSteps.CreateMoveUpDownStep(GrindSteps.P2MainBattleStep));
	}
}
