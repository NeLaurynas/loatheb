using Loatheb.win32;
namespace Loatheb;

public class MouseCtrl
{
	private readonly Sys _sys;
	private readonly Cfg _cfg;
	private readonly Random _rnd;

	private Structures.INPUT[] _moveInputs = null!;
	private Structures.INPUT[] _clickDown = null!;
	private Structures.INPUT[] _clickUp = null!;

	public MouseCtrl(Sys sys, Cfg cfg)
	{
		_sys = sys;
		_cfg = cfg;
		_rnd = new Random();
	}

	public void Initialize()
	{
		_moveInputs = new Structures.INPUT[_cfg.MouseInputBatch];
		_clickDown = new Structures.INPUT[1];
		_clickUp = new Structures.INPUT[1];

		for (var i = 0; i < _cfg.MouseInputBatch; i++)
		{
			var mouseInput = new Structures.MOUSEINPUT();
			mouseInput.dwFlags = Structures.MOUSEEVENTF.MOVE | Structures.MOUSEEVENTF.ABSOLUTE;

			var input = new Structures.INPUT();
			input.type = Structures.INPUT_MOUSE;
			input.U = new Structures.InputUnion {mi = mouseInput};
			_moveInputs[i] = input;
		}

		var mouseDown = new Structures.MOUSEINPUT();
		mouseDown.dwFlags = Structures.MOUSEEVENTF.LEFTDOWN;

		var mouseDownInput = new Structures.INPUT();
		mouseDownInput.type = Structures.INPUT_MOUSE;
		mouseDownInput.U = new Structures.InputUnion {mi = mouseDown};
		_clickDown[0] = mouseDownInput;

		var mouseUp = new Structures.MOUSEINPUT();
		mouseUp.dwFlags = Structures.MOUSEEVENTF.LEFTUP;

		var mouseUpInput = new Structures.INPUT();
		mouseUpInput.type = Structures.INPUT_MOUSE;
		mouseUpInput.U = new Structures.InputUnion {mi = mouseUp};
		_clickUp[0] = mouseUpInput;
	}

	public void Move(int x, int y)
	{
		if (x > _sys.ResX) Console.WriteLine($"WARN - {nameof(MouseCtrl)} - cursor move to {x} is out of screen res x {_sys.ResX}");
		if (y > _sys.ResY) Console.WriteLine($"WARN - {nameof(MouseCtrl)} - cursor move to {y} is out of screen res y {_sys.ResY}");

		var cursosPos = Win32Api.GetCursorPosition();

		double pixelsToMoveX = x - cursosPos.X;
		double pixelsToMoveY = y - cursosPos.Y;

		Console.WriteLine($"Moving mouse from {cursosPos.X} / {cursosPos.Y} to {x} / {y}");

		var stepsX = Math.Abs(pixelsToMoveX / (_cfg.MouseRndMovePxUpperBound / 2.0) / _cfg.MouseInputBatch);
		var stepsY = Math.Abs(pixelsToMoveY / (_cfg.MouseRndMovePxUpperBound / 2.0) / _cfg.MouseInputBatch);
		var steps = Math.Max(Math.Max(stepsX, stepsY), 1);

		double newPosX = cursosPos.X;
		double newPosY = cursosPos.Y;

		for (var i = 0; i < steps; i += +_cfg.MouseInputBatch)
		{
			for (int j = 0; j < _cfg.MouseInputBatch; j++)
			{
				if (i + _cfg.MouseInputBatch >= steps && j + 1 == _cfg.MouseInputBatch)
				{
					newPosX = x;
					newPosY = y;
				}
				else
				{
					newPosX += pixelsToMoveX / steps;
					newPosY += pixelsToMoveY / steps;
				}

				_moveInputs[j].U.mi.dx = _convertCoordinates((int) newPosX, _sys.ResX);
				_moveInputs[j].U.mi.dy = _convertCoordinates((int) newPosY, _sys.ResY);
			}

			if (_rnd.Next(100) < _cfg.MouseSleepChance)
				Thread.Sleep(1);

			Win32Api.SendInput((uint) _moveInputs.Length, _moveInputs, Structures.INPUT.Size);
		}
	}

	public void Click()
	{
		Win32Api.SendInput((uint) _clickDown.Length, _clickDown, Structures.INPUT.Size);
		Thread.Sleep(_rnd.Next(100, 120));
		Win32Api.SendInput((uint) _clickUp.Length, _clickUp, Structures.INPUT.Size);
	}

	private int _convertCoordinates(int coordinate, int resolution)
	{
		return 65536 * coordinate / resolution + 1;
	}
}
