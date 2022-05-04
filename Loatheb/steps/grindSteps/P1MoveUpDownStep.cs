namespace Loatheb.steps.grindSteps;

public class P1MoveUpDownStep : StepBase
{
	public override async Task<StepBase?> Execute()
	{
		var upCount = DI.Rnd.Next(150, 300);
		DI.MouseCtrl.MoveFromCenter(y: upCount);
		DI.MouseCtrl.Click();
		Thread.Sleep(1000);
		
		if (DI.Rnd.Next(100) < 34)
		{
			upCount = DI.Rnd.Next(150, 300);
			DI.MouseCtrl.MoveFromCenter(y: -upCount);
			DI.MouseCtrl.Click();
		}
		else
		{
			Thread.Sleep(100);
		}
		
		Thread.Sleep(DI.Cfg.DelayAfterUpAndDown);

		return GrindSteps.P1MainBattleStep;
	}

	public override void AfterExec()
	{
		ResetState();
	}
}
