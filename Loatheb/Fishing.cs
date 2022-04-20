using Loatheb.win32;
namespace Loatheb;

public class Fishing
{
	private readonly Images _images;
	private readonly KbdCtrl _kbdCtrl;
	private readonly OpenCV _openCv;
	private readonly Repairing _repairing;
	private int _iter; 
	
	public Fishing(Images images, KbdCtrl kbdCtrl, OpenCV openCv, Repairing repairing)
	{
		_images = images;
		_kbdCtrl = kbdCtrl;
		_openCv = openCv;
		_repairing = repairing;
	}

	public async Task Start()
	{
		_iter = -1;
		Console.WriteLine("Activate Lost Ark window (don't move it), and set mouse over water");
		for (int i = 3; i > 0; i--)
		{
			Console.WriteLine($"Will begin in {i} seconds.");
			Thread.Sleep(1000);
		}

		do
		{
			_iter++;

			if (Every(4) && _repairing.NeedsRepairingTool())
			{
				_repairing.RepairTool();
			}

			if (Every(2) && !LifePointsAvailable())
			{
				Console.WriteLine("Life points not available, have you opened life skills (B)?");
				return;
			}

			if (Every(3) && FishingBuffAvailable())
			{
				Console.WriteLine("Fishing buff is available, casting that");
				_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_F);
				Thread.Sleep(3000);
				var cast = !FishingBuffAvailable();
				Console.WriteLine($"Buff was cast - {cast}");
				continue;
			}

			if (FishingInProgress())
			{
				do
				{
					if (await Baited())
					{
						Console.WriteLine("BAITED!");
						_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_E);
						Thread.Sleep(6000);
						break;
					}
					if (!FishingInProgress())
					{
						Console.WriteLine("DIDN'T CAUGHT ANYTHING, STUPID BOT :(");
						break;
					}
					Thread.Sleep(111);
				}
				while (true);
			}
			else
			{
				Console.WriteLine("Fishing is not in progress, starting fishing");
				_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_E);
				Thread.Sleep(3000);
				var fishing = FishingInProgress();
				Console.WriteLine($"Fishing in progress - {fishing}");
				continue;
			}
			
			Thread.Sleep(111);
		}
		while (true);
	}

	private bool Every(int number)
	{
		return _iter % number == 0;
	}

	private bool LifePointsAvailable()
	{
		Console.WriteLine("Checking if life points are available");
		var (result, _) = _openCv.Match(_images.LifePointsAvailable);
		Console.WriteLine($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.8;
	}
	
	private bool FishingBuffAvailable()
	{
		Console.WriteLine("Checking if fishing buff is available");
		var (result, _) = _openCv.Match(_images.FishBuffReady);
		Console.WriteLine($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.9;
	}

	private bool FishingInProgress()
	{
		Console.WriteLine("Checking if fishing is in progress");
		var (result, _) = _openCv.Match(_images.FishingInProgress);
		Console.WriteLine($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.85;
	}

	private async Task<bool> Baited()
	{
		var mark1Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark1);
			Console.WriteLine($"MARK 1 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});		
		var mark2Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark2);
			Console.WriteLine($"MARK 2 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});	
		var mark3Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark3);
			Console.WriteLine($"MARK 3 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});
		
		var mark1 = await mark1Task;
		var mark2 = await mark2Task;
		var mark3 = await mark3Task;

		return (mark1 || mark2 || mark3);
	}
}
