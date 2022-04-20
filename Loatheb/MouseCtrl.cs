using System.Drawing;
using Loatheb.win32;
namespace Loatheb;

public class MouseCtrl
{
	private readonly Sys _sys;
	private readonly Cfg _cfg;
	private readonly Random _rnd;

	private Structures.INPUT[] _moveInputs = null!;
	private Structures.INPUT[] _clickInputs = null!;

	public MouseCtrl(Sys sys, Cfg cfg)
	{
		_sys = sys;
		_cfg = cfg;
		_rnd = new Random();
	}

	public void Initialize()
	{
		_moveInputs = new Structures.INPUT[_cfg.MouseInputBatch];
		_clickInputs = new Structures.INPUT[1];

		for (var i = 0; i < _cfg.MouseInputBatch; i++)
		{
			var mouseInput = new Structures.MOUSEINPUT();
			mouseInput.dwFlags = Structures.MOUSEEVENTF.MOVE | Structures.MOUSEEVENTF.ABSOLUTE;

			var input = new Structures.INPUT();
			input.type = Structures.INPUT_MOUSE;
			input.U = new Structures.InputUnion {mi = mouseInput};
			_moveInputs[i] = input;
		}

		var mouseClick = new Structures.MOUSEINPUT();

		var mouseInputClick = new Structures.INPUT();
		mouseInputClick.type = Structures.INPUT_MOUSE;
		mouseInputClick.U = new Structures.InputUnion {mi = mouseClick};
		_clickInputs[0] = mouseInputClick;
	}

	public void Move(Point[] locations)
	{
		Move(locations[0].X, locations[0].Y);
	}

	public void MoveFromCenter(int x = 0, int y = 0)
	{
		var newPosX = _sys.LAScreenX + _sys.LAScreenWidth / 2 + x;
		var newPosY = _sys.LAScreenY + _sys.LAScreenHeight / 2 + y;
		Move(newPosX, newPosY);
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

	public void SafePosition()
	{
		Move(_sys.LAScreenX + _rnd.Next(10, 200), _sys.LAScreenY + _rnd.Next(10, 200));
	}

	public void Click()
	{
		_clickInputs[0].U.mi.dwFlags = Structures.MOUSEEVENTF.LEFTDOWN;
		_clickInputs[0].U.mi.time = 0;
		Win32Api.SendInput((uint) _clickInputs.Length, _clickInputs, Structures.INPUT.Size);
		Thread.Sleep(_rnd.Next(50, 120));
		_clickInputs[0].U.mi.dwFlags = Structures.MOUSEEVENTF.LEFTUP;
		Win32Api.SendInput((uint) _clickInputs.Length, _clickInputs, Structures.INPUT.Size);
	}

	private int _convertCoordinates(int coordinate, int resolution)
	{
		return 65536 * coordinate / resolution + 1;
	}
}
