using Emgu.CV;
using Emgu.CV.Structure;
using Loatheb.win32;
namespace Loatheb;

public static class MainBattleUtils
{
	public static Dictionary<Skills, Image<Bgr, byte>> SkillMap = new()
	{
		{
			Skills.Q, DI.Images.Qavail
		},
		{
			Skills.W, DI.Images.Wavail
		},
		{
			Skills.E, DI.Images.Eavail
		},
		{
			Skills.R, DI.Images.Ravail
		},
		{
			Skills.A, DI.Images.Aavail
		},
		{
			Skills.S, DI.Images.Savail
		},
		{
			Skills.D, DI.Images.Davail
		},
		{
			Skills.F, DI.Images.Favail
		}
	};

	public static async Task<Skills[]> GetAvailableSkills()
	{
		DI.Logger.Log("Getting available skills");
		
		var tasks = new List<Task<Skills>>(SkillMap.Count);

		tasks.AddRange(SkillMap.Select(skill =>
			Task.Run(() => DI.OpenCV.IsMatching(skill.Value, 985, 965, 255, 110, 0.95) ? skill.Key : Skills.NONE)));

		await Task.WhenAll(tasks);

		return tasks.Where(x => x.Result != Skills.NONE).Select(x => x.Result).Append(Skills.C).ToArray();
	}
	
	public static void MoveMouseIntoPosition(Position position)
	{
		var offset = DI.Rnd.Next(120, 220);
		switch (position)
		{
			case Position.Top:
				DI.MouseCtrl.MoveFromCenter(y: -offset);
				break;
			case Position.Bottom:
				DI.MouseCtrl.MoveFromCenter(y: offset);
				break;
			case Position.Left:
				DI.MouseCtrl.MoveFromCenter(-offset);
				break;
			case Position.Right:
				DI.MouseCtrl.MoveFromCenter(offset);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(position), position, null);
		}
	}
	
	public static void PressSkillAndWait(Skills skill)
	{
		var key = skill switch
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
		
		DI.Logger.Log($"Casting skill {Enum.GetName(skill)}");

		DI.KbdCtrl.PressKey(key);
		if (DI.Rnd.Next(100) < 50)
		{
			Thread.Sleep(20);
			DI.KbdCtrl.PressKey(key);
		}
		
		Thread.Sleep(DI.Cfg.DelayBetweenSkills);
	}
}
