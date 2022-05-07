namespace Loatheb.steps.grindSteps;

public class P1MainBattleStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		try
		{
			var position = (Position)DI.Rnd.Next(4);
			
			do
			{
				var availableSkills = await MainBattleUtils.GetAvailableSkills();

				foreach (var skill in availableSkills)
				{
					MainBattleUtils.MoveMouseIntoPosition(position);
					Thread.Sleep(1);
					MainBattleUtils.PressSkillAndWait(skill);
					position = position.NextPosition();
				}

				if (DI.Rnd.Next(100) < DI.Cfg.ChanceToMove)
					DI.MouseCtrl.Click();
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
		// TODO: check if it's running!
		DI.Logger.Log("Checking if it's time to bail");
		var p19Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.Prog19, 5, 30, 280, 300, 0.85));
		var p20Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.Prog20, 5, 30, 280, 300, 0.85));
		var p21Task = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.Prog21, 5, 30, 280, 300, 0.85));

		var a = await p19Task;
		var b = await p20Task;
		var c = await p21Task;
		return a || b || c;
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
