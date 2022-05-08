using Emgu.CV.CvEnum;
namespace Loatheb.steps.grindSteps;

public class P2MainBattleStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		var beginTime = DateTime.Now;

		await CheckForBossMinimapAndMove();

		var iter = 1;
		var position = (Position)DI.Rnd.Next(4);
		
		do
		{
			DI.Logger.Log($"Iteration {iter}");
			if (DI.Overlord.Running == false)
			{
				DI.Logger.Log("Overlord stopping, leaving");
				return GrindSteps.LeaveChaosDungeonStep;
			}
			if (beginTime.AddSeconds(80) < DateTime.Now)
			{
				DI.Logger.Log("Time's up, leaving");
				return GrindSteps.LeaveChaosDungeonStep;
			}
			if (await IsHPLow()) return GrindSteps.LeaveChaosDungeonStep;

			if (Utils.Every(5, iter) && Utils.DeadWindowShowing())
				return UtilSteps.CreateLeaveFromDeadWindowStep(RepairEquipmentSteps.RepairEquipmentBegin);

			if (Utils.Every(9, iter))
			{
				DI.Logger.Log("Checking for boss and moving");
				await CheckForBossMinimapAndMove();
			}
			
			// todo: check nearby hp bars, if found - proceed, if not - move to boss?
			var availableSkills = await MainBattleUtils.GetAvailableSkills();

			foreach (var skill in availableSkills)
			{
				MainBattleUtils.MoveMouseIntoPosition(position);
				Thread.Sleep(1);
				MainBattleUtils.PressSkillAndWait(skill);
				position = position.NextPosition();
			}

			if (DI.Rnd.Next(100) < 20)
				DI.MouseCtrl.Click();

			iter++;
		}
		while (true);
	}

	public async Task<bool> IsHPLow()
	{
		DI.Logger.Log("Checking if HP is low");
		var task1 = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.LowHP1, ScreenLocations.HPBarStart, confidence: 0.96d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		var task2 = Task.Run(() => DI.OpenCV.IsMatching(DI.Images.LowHP2, ScreenLocations.HPBarStart, confidence: 0.96d, templateMatchingType: TemplateMatchingType.CcorrNormed));

		var a = await task1;
		var b = await task2;

		return a || b;
	}

	private async Task CheckForBossMinimapAndMove()
	{
		DI.Logger.Log("Checking for boss on minimap");
		var mini1Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.BossMini1, ScreenLocations.Minimap, confidence: 0.98d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		var mini2Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.BossMini2, ScreenLocations.Minimap, confidence: 0.98d, templateMatchingType: TemplateMatchingType.CcorrNormed));
		var mini3Task = Task.Run(() => DI.OpenCV.IsMatchingWhere(DI.Images.BossMini3, ScreenLocations.Minimap, confidence: 0.98d, templateMatchingType: TemplateMatchingType.CcorrNormed));

		var miniTasks = await Task.WhenAll(mini1Task, mini2Task, mini3Task);
		var foundFirst = miniTasks.FirstOrDefault(x => x.isMatching);
		if (foundFirst.isMatching)
		{
			DI.MouseCtrl.MoveMinimapDistance(foundFirst.maxLocations);
			DI.Logger.Log("Found boss on minimap");
		}
		else 
			DI.Logger.Log("Not found boss on minimap");
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
