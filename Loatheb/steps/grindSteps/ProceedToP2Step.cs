using Emgu.CV.CvEnum;
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
			SleepDurationAfterExecution = 20,
			MaxIter = 4
		};
	}

	public override async Task<StepBase?> Execute()
	{
		DI.Logger.Log("Checking for enter portal icon");
		if (DI.OpenCV.IsMatching(DI.Images.MoveIcon, ScreenLocations.MiddleOfTheScreen, 0.85d))
		{
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			if (await TryWaitingForLoadAndCheckIfInP2())
				return GrindSteps.P2MainBattleStep;
		}
		
		// DI.Logger.Log("Checking for blue portal");
		// var bluePortal1Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal1, 0.6d, templateMatchingType: TemplateMatchingType.CcorrNormed)); 
		// var bluePortal2Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal2, 0.6d, templateMatchingType: TemplateMatchingType.CcorrNormed)); 
		// var bluePortal3Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal3, 0.6d, templateMatchingType: TemplateMatchingType.CcorrNormed)); 
		// var bluePortal4Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.Portal4, 0.6d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		//
		// var tasks = await Task.WhenAll(bluePortal1Task, bluePortal2Task, bluePortal3Task, bluePortal4Task);
		//
		// var matchesPortal = tasks.FirstOrDefault(x => x.isMatching);
		// if (matchesPortal.isMatching)
		// {
		// 	DI.Logger.Log("Found, moving to it");
		// 	DI.MouseCtrl.MoveAndClick(matchesPortal.maxLocations);
		// 	Thread.Sleep(500);
		// 	
		// 	DI.Logger.Log("Checking for enter portal icon");
		// 	if (DI.OpenCV.IsMatching(DI.Images.MoveIcon, ScreenLocations.MiddleOfTheScreen, 0.85d))
		// 	{
		// 		DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
		// 		DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
		// 		if (await TryWaitingForLoadAndCheckIfInP2())
		// 			return GrindSteps.P2MainBattleStep;
		// 	}
		// 	
		// 	if (await TryWaitingForLoadAndCheckIfInP2())
		// 		return GrindSteps.P2MainBattleStep;
		// }
		//
		// DI.Logger.Log("Checking for blue portal minimap");
		// var blueMini1Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini1, ScreenLocations.Minimap, 0.96d, TemplateMatchingType.CcorrNormed));
		// var blueMini2Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini2, ScreenLocations.Minimap, 0.96d, TemplateMatchingType.CcorrNormed));
		// var blueMini3Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini3, ScreenLocations.Minimap, 0.96d, TemplateMatchingType.CcorrNormed));
		// var blueMini4Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.PortalMini4, ScreenLocations.Minimap, 0.96d, TemplateMatchingType.CcorrNormed));
		//
		// var tasksMini = await Task.WhenAll(blueMini1Task, blueMini2Task, blueMini3Task, blueMini4Task);
		//
		// var matchesMini = tasksMini.FirstOrDefault(x => x.isMatching);
		// if (matchesMini.isMatching)
		// {
		// 	DI.Logger.Log("Found minimap, moving to it");
		// 	DI.MouseCtrl.MoveMinimapDistance(matchesMini.maxLocations);
		// 	if (await TryWaitingForLoadAndCheckIfInP2())
		// 		return GrindSteps.P2MainBattleStep;
		// }
		
		if (DI.OpenCV.IsMatching(DI.Images.MoveIcon, ScreenLocations.MiddleOfTheScreen, 0.85d))
		{
			DI.Logger.Log("Found enter icon, sending G");
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
			if (await TryWaitingForLoadAndCheckIfInP2())
				return GrindSteps.P2MainBattleStep;
		}
		//
		// DI.MouseCtrl.MoveFromCenter(0, 100);
		// DI.MouseCtrl.Click();

		return this;
	}

	private async Task<bool> TryWaitingForLoadAndCheckIfInP2()
	{
		Thread.Sleep(250);
		Utils.TryUntilTrue(Utils.IsLoaded, 40, 1000);
		await GrindSteps.CreateMoveUpDownStep().Execute();
		Thread.Sleep(1000);

		DI.Logger.Log("Checking if boss on minimap is present");
		var mini1Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.BossMini1, ScreenLocations.Minimap, 0.92d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		var mini2Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.BossMini1, ScreenLocations.Minimap, 0.92d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		var mini3Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.BossMini1, ScreenLocations.Minimap, 0.92d, templateMatchingType: TemplateMatchingType.CcorrNormed));

		var miniTasks = await Task.WhenAll(mini1Task, mini2Task, mini3Task);
		
		var result = miniTasks.Any(x => x);
		
		if (result) ResetState();

		return result;
	}
}
