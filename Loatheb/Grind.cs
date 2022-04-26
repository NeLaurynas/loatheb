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
	private readonly Cfg _cfg;
	private readonly Random _rnd;
	private readonly Dictionary<Skills, Image<Bgr, byte>> _skills;

	public Grind(MouseCtrl mouseCtrl, KbdCtrl kbdCtrl, Images images, OpenCV openCv, Repairing repairing, Cfg cfg)
	{
		_mouseCtrl = mouseCtrl;
		_kbdCtrl = kbdCtrl;
		_images = images;
		_openCv = openCv;
		_repairing = repairing;
		_cfg = cfg;
		_rnd = new Random();

		_skills = new Dictionary<Skills, Image<Bgr, byte>>
		{
			{
				Skills.Q, _images.Qavail
			},
			{
				Skills.W, _images.Wavail
			},
			{
				Skills.E, _images.Eavail
			},
			{
				Skills.R, _images.Ravail
			},
			{
				Skills.A, _images.Aavail
			},
			{
				Skills.S, _images.Savail
			},
			{
				Skills.D, _images.Davail
			},
			{
				Skills.F, _images.Favail
			}
		};
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
			if (await EnterChaosDungeon())
			{
				Console.WriteLine("Entered chaos dungeon");
				var insideChaos = false;
				for (int i = 0; i < 20; i++)
				{
					Thread.Sleep(1000);
					if (InsideChaosDungeon())
					{
						insideChaos = true;
						break;
					}
				}

				if (insideChaos)
				{
					await JeffTheBattleRoutine();
					continue;
				}
				else
				{
					Console.WriteLine("Not inside chaos dungeon, what?");
					Thread.Sleep(10000);
				}
			}
			else
			{
				Console.WriteLine("Couldn't enter chaos dungeon");
				Thread.Sleep(10000);
			}
		}
		while (true);
	}

	public async Task<bool> EnterChaosDungeon()
	{
		_mouseCtrl.SafePosition();
		for (int i = 0; i < 5; i++)
		{
			var (statueOk, statueLocation) = await ChaosDungeonStatue();
			if (statueOk)
			{
				Console.WriteLine("Statue found, opening chaos dungeon window");
				_mouseCtrl.Move(statueLocation);
				Thread.Sleep(100);
				_mouseCtrl.Click();
				break;
			}
			else
			{
				Console.WriteLine("Statue NOT found, pressing G in hope of opening chaos dungeon window");
				_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_G);
				if (ChaosDungeonShowing())
				{
					break;
				}
			}
			Thread.Sleep(2000);
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
					for (var i = 0; i < 10; i++)
					{
						Thread.Sleep(200);
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

	public bool InsideChaosDungeon()
	{
		Console.WriteLine("Checking if inside chaos dungeon");
		return _openCv.IsMatching(_images.InsideDungeon, 0.9, ScreenPart.Left);
	}

	public bool ClickOk()
	{
		Console.WriteLine("Trying to click Ok");
		var (ok, location) = _openCv.IsMatchingWhere(_images.OkBtn);
		if (!ok) return false;

		_mouseCtrl.Move(location);
		Thread.Sleep(100);
		_mouseCtrl.Click();
		return true;
	}

	public async Task JeffTheBattleRoutine()
	{
		// move up and down
		var upCount = _rnd.Next(150, 300);
		_mouseCtrl.MoveFromCenter(y: upCount);
		_mouseCtrl.Click();
		Thread.Sleep(1000);
		if (_rnd.Next(100) < 34)
		{
			upCount = _rnd.Next(150, 300);
			_mouseCtrl.MoveFromCenter(y: -upCount);
			_mouseCtrl.Click();
		}
		else
		{
			Thread.Sleep(100);
		}
		Thread.Sleep(_cfg.DelayAfterUpAndDown);

		var position = (Position)_rnd.Next(4);

		do
		{
			Console.WriteLine("Getting available skills");
			var availableSkills = await GetAvailableSkills();

			foreach (var skill in availableSkills)
			{
				MoveMouseIntoPosition(position);
				Thread.Sleep(1);
				PressSkillAndWait(skill);
				position = position.NextPosition();
			}

			if (_rnd.Next(100) < _cfg.ChanceToMove)
				_mouseCtrl.Click();
		}
		while (!await TimeToBail());

		if (!Bail())
		{
			Console.WriteLine("Couldn't leave the dungeon!");
			// todo: probably also quit Lost Ark process?
			throw new Exception("FUUUUUUCK, couldn't leave the dungeon");
		}
		
		Thread.Sleep(6000);

		while (InLoadingScreen())
		{
			Console.WriteLine("Still in loading screen");
			Thread.Sleep(1000);
		}
	}

	public async Task<Skills[]> GetAvailableSkills()
	{
		var tasks = new List<Task<Skills>>(_skills.Count);

		tasks.AddRange(_skills.Select(skill =>
			Task.Run(() => _openCv.IsMatching(skill.Value, 0.95, ScreenPart.Bottom) ? skill.Key : Skills.NONE)));

		await Task.WhenAll(tasks);

		return tasks.Where(x => x.Result != Skills.NONE).Select(x => x.Result).Append(Skills.C).ToArray();
	}

	public async Task<bool> TimeToBail()
	{
		Console.WriteLine("Checking if it's time to bail");
		var p19Task = Task.Run(() =>
		{
			return _openCv.IsMatching(_images.Prog19, 0.85, ScreenPart.Left);
		});
		var p20Task = Task.Run(() =>
		{
			return _openCv.IsMatching(_images.Prog20, 0.85, ScreenPart.Left);
		});
		var p21Task = Task.Run(() =>
		{
			return _openCv.IsMatching(_images.Prog21, 0.85, ScreenPart.Left);
		});

		var a = await p19Task;
		var b = await p20Task;
		var c = await p21Task;
		return a || b || c;
	}

	public bool InLoadingScreen()
	{
		Console.WriteLine("Checking if in Loading Screen");
		return _openCv.IsMatching(_images.Loading, 0.95, ScreenPart.Bottom);
	}

	public bool Bail()
	{
		Console.WriteLine("Trying to locate Leave button");
		var (leaveOk, leaveLoc) = _openCv.IsMatchingWhere(_images.LeaveBtn, 0.9, ScreenPart.Left);
		if (!leaveOk) return false;

		_mouseCtrl.Move(leaveLoc);
		Thread.Sleep(100);
		_mouseCtrl.Click();

		var clickedOk = false;

		for (int i = 0; i < 3; i++)
		{
			Thread.Sleep(240);
			if (ClickOk())
			{
				clickedOk = true;
				break;
			}
		}

		return clickedOk;
	}

	public void MoveMouseIntoPosition(Position position)
	{
		var offset = _rnd.Next(120, 220);
		switch (position)
		{
			case Position.Top:
				_mouseCtrl.MoveFromCenter(y: -offset);
				break;
			case Position.Bottom:
				_mouseCtrl.MoveFromCenter(y: offset);
				break;
			case Position.Left:
				_mouseCtrl.MoveFromCenter(-offset);
				break;
			case Position.Right:
				_mouseCtrl.MoveFromCenter(offset);
				break;
		}
	}

	public void PressSkillAndWait(Skills skill)
	{
		Structures.VirtualKeyShort key = skill switch
		{
			Skills.Q => Structures.VirtualKeyShort.KEY_Q,
			Skills.W => Structures.VirtualKeyShort.KEY_W,
			Skills.E => Structures.VirtualKeyShort.KEY_E,
			Skills.R => Structures.VirtualKeyShort.KEY_R,
			Skills.A => Structures.VirtualKeyShort.KEY_A,
			Skills.S => Structures.VirtualKeyShort.KEY_S,
			Skills.D => Structures.VirtualKeyShort.KEY_D,
			Skills.F => Structures.VirtualKeyShort.KEY_F,
			Skills.C => Structures.VirtualKeyShort.KEY_C,
			_ => Structures.VirtualKeyShort.KEY_R // yolo
		};
		
		Console.WriteLine($"Casting skill {Enum.GetName(skill)}");

		_kbdCtrl.PressKey(key);
		if (_rnd.Next(100) < 50)
		{
			Thread.Sleep(20);
			_kbdCtrl.PressKey(key);
		}
		
		Thread.Sleep(_cfg.DelayBetweenSkills);
	}
}
