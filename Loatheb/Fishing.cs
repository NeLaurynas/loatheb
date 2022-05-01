using Loatheb.win32;
namespace Loatheb;

public class Fishing
{
	private readonly Images _images;
	private readonly KbdCtrl _kbdCtrl;
	private readonly OpenCV _openCv;
	private readonly Repairing _repairing;
	private readonly MouseCtrl _mouseCtrl;
	private readonly Logger _logger;
	
	private int _iter;
	private readonly Random _rnd;
	
	public Fishing(Images images, KbdCtrl kbdCtrl, OpenCV openCv, Repairing repairing, MouseCtrl mouseCtrl, Logger logger)
	{
		_images = images;
		_kbdCtrl = kbdCtrl;
		_openCv = openCv;
		_repairing = repairing;
		_mouseCtrl = mouseCtrl;
		_logger = logger;
		_rnd = new Random();
	}

	public async Task Start()
	{
		_iter = -1;
		_logger.Log("Activate Lost Ark window (don't move it), and make sure water is to the right");
		for (int i = 3; i > 0; i--)
		{
			_logger.Log($"Will begin in {i} seconds.");
			Thread.Sleep(1000);
		}

		do
		{
			if (Every(6))
				_mouseCtrl.MoveFromCenter(x: _rnd.Next(300, 450));
			
			_iter++;

			if (Every(4) && _repairing.NeedsRepairingToolOld())
			{
				_repairing.OldRepairToolOld();
				_mouseCtrl.MoveFromCenter(333);
			}
			
			if (FishingGreyedOut() || !LifePointsAvailable())
			{
				_logger.Log("Fishing skill was greyed out - out of life points?");
				return;
			}

			if (Every(3) && FishingBuffAvailable())
			{
				_logger.Log("Fishing buff is available, casting that");
				_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_F);
				Thread.Sleep(3000);
				var cast = !FishingBuffAvailable();
				_logger.Log($"Buff was cast - {cast}");
				continue;
			}

			if (FishingInProgress())
			{
				do
				{
					if (await Baited())
					{
						_logger.Log("BAITED!");
						_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_E);
						Thread.Sleep(6000);
						break;
					}
					if (!FishingInProgress())
					{
						_logger.Log("DIDN'T CAUGHT ANYTHING, STUPID BOT :(");
						break;
					}
					Thread.Sleep(111);
				}
				while (true);
			}
			else
			{
				_logger.Log("Fishing is not in progress, starting fishing");
				_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_E);
				Thread.Sleep(3000);
				var fishing = FishingInProgress();
				_logger.Log($"Fishing in progress - {fishing}");
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
		_logger.Log("Checking if life points are available");
		var (result, _) = _openCv.Match(_images.LifePointsAvailable, ScreenPart.Bottom);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.8;
	}

	private bool FishingGreyedOut()
	{
		_logger.Log("Checking if fishing is greyed out");
		var (result, _) = _openCv.Match(_images.FishingGreyedOut, ScreenPart.Bottom);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.9;
	}
	
	private bool FishingBuffAvailable()
	{
		_logger.Log("Checking if fishing buff is available");
		var (result, _) = _openCv.Match(_images.FishBuffReady, ScreenPart.Bottom);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.9;
	}

	private bool FishingInProgress()
	{
		_logger.Log("Checking if fishing is in progress");
		var (result, _) = _openCv.Match(_images.FishingInProgress, ScreenPart.Bottom);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] > 0.85;
	}

	private async Task<bool> Baited()
	{
		var mark1Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark1);
			_logger.Log($"MARK 1 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});		
		var mark2Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark2);
			_logger.Log($"MARK 2 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});	
		var mark3Task = Task.Run(() =>
		{
			var (result, _) = _openCv.Match(_images.FishingMark3);
			_logger.Log($"MARK 3 - C - {result.Length}, V - {result.FirstOrDefault()}");
			return result.Length == 1 && result[0] > 0.66;
		});
		
		var mark1 = await mark1Task;
		var mark2 = await mark2Task;
		var mark3 = await mark3Task;

		return (mark1 || mark2 || mark3);
	}
}
