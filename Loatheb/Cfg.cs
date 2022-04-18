namespace Loatheb;

public class Cfg
{
	public int MouseInputBatch { get; set; }
	public int MouseSleepChance { get; set; }
	public int MouseRndMovePxUpperBound { get; set; }

	public void Initialize()
	{
		// TODO: read config
		MouseInputBatch = 3;
		MouseSleepChance = 10;
		MouseRndMovePxUpperBound = 5;
	}
}
