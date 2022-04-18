using Loatheb.win32;
namespace Loatheb;

public class MouseCtrl
{
	private readonly Sys _sys;
	private readonly Cfg _cfg;
	private readonly Random _rnd;

	private Structures.INPUT[] _inputs;

	public MouseCtrl(Sys sys, Cfg cfg)
	{
		_sys = sys;
		_cfg = cfg;
		_rnd = new Random();
	}

	public void Initialize()
	{
		_inputs = new Structures.INPUT[_cfg.MouseInputBatch];

		for (int i = 0; i < _cfg.MouseInputBatch; i++)
		{
			var mouseInput = new Structures.MOUSEINPUT();
			mouseInput.dwFlags = Structures.MOUSEEVENTF.MOVE | Structures.MOUSEEVENTF.ABSOLUTE;

			var input = new Structures.INPUT();
			input.type = Structures.INPUT_MOUSE;
			input.U = new Structures.InputUnion {mi = mouseInput};
			_inputs[i] = input;
		}

		// move to middle of the screen
		Move(_sys.ResX / 2, _sys.ResY / 2);
	}

	public void Move(int x, int y)
	{
		// if (x > _sys.ResX) Console.WriteLine($"WARN - {nameof(MouseCtrl)} - cursor move to {x} is out of screen res x {_sys.ResX}");
		// if (y > _sys.ResY) Console.WriteLine($"WARN - {nameof(MouseCtrl)} - cursor move to {y} is out of screen res y {_sys.ResY}");
		//
		// var pixelsToMoveX = Math.Abs(PosX - x);
		// var pixelsToMoveY = Math.Abs(PosY - y);
		//
		// Console.WriteLine($"FROM {PosX} / {PosY} to {x} / {y}");
		//
		// var stepsX = pixelsToMoveX / (_cfg.MouseRndMovePxUpperBound / 2) / _cfg.MouseInputBatch;
		// var stepsY = pixelsToMoveY / (_cfg.MouseRndMovePxUpperBound / 2) / _cfg.MouseInputBatch;
		// var steps = Math.Max(Math.Max(stepsX, stepsY), 1);
		// Console.WriteLine($"StepsX = {stepsX}, StepsY = {stepsY}, Steps = {steps}");
		//
		// for (var i = 0; i <= steps; i += +_cfg.MouseInputBatch)
		// {
		// 	for (int j = 0; j < _cfg.MouseInputBatch; j++)
		// 	{
		// 		var newPosX = PosX + pixelsToMoveX / steps * Math.Min(i + j + 1, steps);
		// 		var newPosY = PosY + pixelsToMoveY / steps * Math.Min(i + j + 1, steps);
		// 		_inputs[j].U.mi.dx = _convertCoordinates(newPosX, _sys.ResX);
		// 		_inputs[j].U.mi.dy = _convertCoordinates(newPosY, _sys.ResY);
		// 		Console.WriteLine($"-- TO {newPosX} / {newPosY}");
		// 	}
		//
		// 	if (_rnd.Next(100) < _cfg.MouseSleepChance)
		// 		Thread.Sleep(1);
		//
		// 	Win32Api.SendInput((uint) _inputs.Length, _inputs, Structures.INPUT.Size);
		// }
	}

	private int _convertCoordinates(int coordinate, int resolution)
	{
		return 65536 * coordinate / resolution + 1;
	}
}
