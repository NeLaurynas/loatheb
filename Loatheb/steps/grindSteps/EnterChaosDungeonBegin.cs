using Emgu.CV;
using Emgu.CV.Structure;
using Loatheb.win32;
namespace Loatheb.steps.grindSteps;

public class EnterChaosDungeonBegin : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public EnterChaosDungeonBegin()
	{
		State = new StepStateBase
		{
			MaxIter = 5,
			SleepDurationBeforeExecuting = 500
		};
	}
	
	public override async Task<StepBase?> Execute()
	{
		if (Utils.InsideChaosDungeon())
		{
			return GrindSteps.LeaveChaosDungeonStep;
		}
		
		DI.MouseCtrl.SafePosition();

		for (int i = 0; i < 5; i++)
		{
			var (statueOk, statueLocation) = await ChaosDungeonStatue();
			if (statueOk)
			{
				DI.Logger.Log("Statue found, opening chaos dungeon window");
				DI.MouseCtrl.Move(statueLocation);
				Thread.Sleep(100);
				DI.MouseCtrl.Click();
				ResetState();
				return GrindSteps.ClickEnterChaosDungeonStep;
			}
			else
			{
				DI.Logger.Log("Statue NOT found, pressing G in hope of opening chaos dungeon window");
				DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
				if (ChaosDungeonUIShowing())
				{
					ResetState();
					return GrindSteps.ClickEnterChaosDungeonStep;
				}
			}
			Thread.Sleep(2000);
		}
		
		return this;
	}

	public async Task<(bool ok, Point[] location)> ChaosDungeonStatue()
	{
		var tasks = new Task<(double[] result, Point[] location)>[11];
		for (var i = 1; i <= 11; i++)
		{
			var i1 = i;
			tasks[i - 1] = Task.Run(() =>
			{
				var (result, location) = DI.OpenCV.Match((DI.Images.GetType().GetField($"Dungeon{i1}")!.GetValue(DI.Images) as Image<Bgr, byte>)!);
				DI.Logger.Log($"DNG {i1} - C - {0.79}, V - ${result.FirstOrDefault()}, # - {result.Length}");
				return (result, location);
			});
		}

		await Task.WhenAll(tasks);
		var mostLikely = tasks.OrderByDescending(x => x.Result.result[0]).First().Result;

		return (mostLikely.result[0] > 0.79, mostLikely.location);
	}
	
	public bool ChaosDungeonUIShowing()
	{
		DI.Logger.Log("Checking if chaos dungeon is showing");
		return DI.OpenCV.IsMatching(DI.Images.ChaosDungeonWindowTitle, 0.9, ScreenPart.Top);
	}
}
