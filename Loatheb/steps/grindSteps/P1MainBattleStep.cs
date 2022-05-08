using Emgu.CV.CvEnum;
namespace Loatheb.steps.grindSteps;

public class P1MainBattleStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		try
		{
			var position = (Position)DI.Rnd.Next(4);
			var start = DateTime.Now;
			
			do
			{
				if (DI.Overlord.Running == false) throw new Exception("Stopping P1");
				
				var availableSkills = await MainBattleUtils.GetAvailableSkills();

				foreach (var skill in availableSkills)
				{
					MainBattleUtils.MoveMouseIntoPosition(position);
					Thread.Sleep(1);
					MainBattleUtils.PressSkillAndWait(skill);
					position = position.NextPosition();
				}

				// random movement in first 30 seconds
				// no random movement
				// if (start.AddSeconds(30) > DateTime.Now && DI.Rnd.Next(100) < DI.Cfg.ChanceToMove)
					// DI.MouseCtrl.Click();
			}
			while (!await CanProceedToP2());

			return GrindSteps.ProceedToP2Step;
		}
		catch (Exception ex)
		{
			DI.Logger.Log($"P1 Main Battle Step exception - {ex.Message}");
			return GrindSteps.LeaveChaosDungeonStep;
		}
	}
	
	public async Task<bool> CanProceedToP2()
	{
		DI.Logger.Log("Checking if it's time to move to P2");
		var redDotMinimapTask = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.RedMini, ScreenLocations.Minimap, 0.95, false, TemplateMatchingType.CcorrNormed));
		var p21Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.Prog21, 5, 30, 280, 300, 0.8));

		var a = await redDotMinimapTask;
		var b = await p21Task;
		return b && !a;
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
