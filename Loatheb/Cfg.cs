namespace Loatheb;

public class Cfg
{
	public int MouseInputBatch { get; set; }
	public int MouseSleepChance { get; set; }
	public int MouseRndMovePxUpperBound { get; set; }
	
	public string FishingLetter { get; set; }
	public string FishingBuffLetter { get; set; }

	public string LAProcessName { get; set; }

	public int DelayAfterUpAndDown { get; set; }
	public int DelayBetweenSkills { get; set; }
	public int ChanceToMove { get; set; }

	public void Initialize()
	{
		// TODO: read config
		MouseInputBatch = 3;
		MouseSleepChance = 10;
		MouseRndMovePxUpperBound = 5;
		LAProcessName = "lostark";
		FishingLetter = "E";
		FishingLetter = "F";

		DelayAfterUpAndDown = 2300;
		DelayBetweenSkills = 400;
		ChanceToMove = 20;
	}
}
