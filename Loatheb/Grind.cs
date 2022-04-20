using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using Loatheb.win32;
namespace Loatheb;

public class Grind
{
	private readonly MouseCtrl _mouseCtrl;
	private readonly KbdCtrl _kbdCtrl;
	private readonly Images _images;
	private readonly OpenCV _openCv;
	private readonly Repairing _repairing;

	public Grind(MouseCtrl mouseCtrl, KbdCtrl kbdCtrl, Images images, OpenCV openCv, Repairing repairing)
	{
		_mouseCtrl = mouseCtrl;
		_kbdCtrl = kbdCtrl;
		_images = images;
		_openCv = openCv;
		_repairing = repairing;
	}

	public async Task Start()
	{
		Console.WriteLine("Active Lost Ark window (don't move it), and stand near chaos dungeon entrance");
		for (var i = 3; i > 0; i--)
		{
			Console.WriteLine($"Will begin in {i} seconds.");
			Thread.Sleep(1000);
		}

		do
		{
			_kbdCtrl.EscapeTwice();
			if (await EnterChaosDungeon())
			{
				Console.WriteLine("Entered chaos dungeon?!");
				break;
			}
			else
			{
				Console.WriteLine("Couldn't enter chaos dungeon");
				break;
			}
		}
		while (true);
	}

	public async Task<bool> EnterChaosDungeon()
	{
		_mouseCtrl.SafePosition();
		var (statueOk, statueLocation) = await ChaosDungeonStatue();
		if (statueOk)
		{
			Console.WriteLine("Statue found, opening chaos dungeon window");
			_mouseCtrl.Move(statueLocation);
			Thread.Sleep(100);
			_mouseCtrl.Click();
		}
		else
		{
			Console.WriteLine("Statue NOT found, pressing G in hope of opening chaos dungeon window");
			_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
		}
		Thread.Sleep(300);

		if (ChaosDungeonShowing())
		{
			var dungeonSelected = false;
			for (var i = 0; i < 2; i++)
			{
				Console.WriteLine("Chaos Dungeon window is showing");
				if (!DungeonSelected())
				{
					var (dungeonCanBeSelected, location) = DungeonNotSelected();
					if (dungeonCanBeSelected)
					{
						_mouseCtrl.Move(location);
						Thread.Sleep(100);
						_mouseCtrl.Click();
						Thread.Sleep(200);
					}
				}
				else
				{
					dungeonSelected = true;
					Console.WriteLine("Dungeon is selected");
					break;
				}
			}

			if (dungeonSelected)
			{
				if (ClickEnter())
				{
					for (var i = 0; i < 5; i++)
					{
						Thread.Sleep(1000);
						if (ClickAccept())
						{
							Console.WriteLine("Clicked Accept, should be in a dungeon!");
							return true;
						}
					}
					Console.WriteLine("Couldn't click Accept");
					return false;
				}
				
				Console.WriteLine("Couldn't click Enter");
				return false;
			}

			Console.WriteLine("Couldn't select dungeon");
			return false;
		}

		Console.WriteLine("Couldn't find chaos dungeon window :(");
		return false;
	}
	public async Task<(bool ok, Point[] location)> ChaosDungeonStatue()
	{
		var tasks = new Task<(double[] result, Point[] location)>[11];
		for (var i = 1; i <= 11; i++)
		{
			var i1 = i;
			tasks[i - 1] = Task.Run(() =>
			{
				var (result, location) = _openCv.Match((_images.GetType().GetField($"Dungeon{i1}")!.GetValue(_images) as Image<Bgr, byte>)!);
				Console.WriteLine($"DNG {i1} - C - {result.Length}, V - ${result.FirstOrDefault()}");
				return (result, location);
			});
		}

		await Task.WhenAll(tasks);
		var mostLikely = tasks.OrderByDescending(x => x.Result.result[0]).First().Result;

		return (mostLikely.result[0] > 0.85, mostLikely.location);
	}

	public bool ChaosDungeonShowing()
	{
		Console.WriteLine("Checking if chaos dungeon is showing");
		return _openCv.IsMatching(_images.ChaosDungeonWindowTitle, 0.9, ScreenPart.Top);
	}

	public bool DungeonSelected()
	{
		Console.WriteLine("Checking if chaos dungeon is selected");
		return _openCv.IsMatching(_images.DungeonSelected, 0.9, ScreenPart.Left);
	}

	public (bool ok, Point[] locations) DungeonNotSelected()
	{
		Console.WriteLine("Checking if chaos dungeon can be selected");
		return _openCv.IsMatchingWhere(_images.DungeonNotSelected, 0.9, ScreenPart.Left);
	}

	public bool ClickEnter()
	{
		Console.WriteLine("Trying to click Enter");
		var (ok, location) = _openCv.IsMatchingWhere(_images.EnterBtn, 0.9, ScreenPart.Right);
		if (!ok) return false;
		_mouseCtrl.Move(location);
		Thread.Sleep(100);
		_mouseCtrl.Click();
		return true;
	}

	public bool ClickAccept()
	{
		Console.WriteLine("Trying to click Accept");
		var (ok, location) = _openCv.IsMatchingWhere(_images.AcceptBtn);
		if (!ok) return false;
		_mouseCtrl.Move(location);
		Thread.Sleep(100);
		_mouseCtrl.Click();
		return true;
	}
}
