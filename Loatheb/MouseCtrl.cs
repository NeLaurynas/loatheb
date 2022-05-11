using System.Drawing;
using Loatheb.win32;
namespace Loatheb;

public class MouseCtrl
{
	private readonly Sys _sys;
	private readonly Cfg _cfg;
	private readonly Random _rnd;
	private readonly Logger _logger;

	private Structures.INPUT[] _moveInputs = null!;
	private Structures.INPUT[] _clickInputs = null!;

	public MouseCtrl(Sys sys, Cfg cfg, Logger logger)
	{
		_sys = sys;
		_cfg = cfg;
		_logger = logger;
		_rnd = new Random();
	}

	public void Initialize()
	{
		_logger.Log("Initializing mouse controller");
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

	public void MoveAndClick(Point[] locations)
	{
		Move(locations);
		Thread.Sleep(100);
		Click();
	}

	// todo: scale in world and then in chaos dungeon completely off
	public void MoveMinimapDistance(Point[] locations)
	{
		const int centerX = 2380;
		const int centerY = 172;
		var loc = locations.FirstOrDefault();
		// System.Diagnostics.Debugger.Break();
		
		if (loc != default)
		{
			var minimapX = loc.X - DI.Sys.LAScreenX;
			var minimapY = loc.Y - DI.Sys.LAScreenY - 210;

			var distanceOnMiniX = minimapX - centerX;
			var distanceOnMiniY = minimapY - centerY;
			var movingRight = distanceOnMiniX > 0;
			var movingTop = distanceOnMiniY < 0;

			int toMoveY = 0;
			do
			{
				int toMoveX;
				if (movingRight)
				{
					toMoveX = distanceOnMiniX > 32 ? 32 : distanceOnMiniX;
					distanceOnMiniX = Math.Max(0, distanceOnMiniX - toMoveX);
				}
				else
				{
					toMoveX = Math.Abs(distanceOnMiniX) > 29 ? -29 : distanceOnMiniX;
					distanceOnMiniX = Math.Min(0, distanceOnMiniX + Math.Abs(toMoveX));
				}

				if (movingTop)
				{
					toMoveY = Math.Abs(distanceOnMiniY) > 20 ? -20 : distanceOnMiniY;
					distanceOnMiniY = Math.Min(0, distanceOnMiniY + Math.Abs(toMoveY));
				}
				else
				{
					toMoveY = distanceOnMiniY > 14 ? 14 : distanceOnMiniY;
					distanceOnMiniY = Math.Max(0, distanceOnMiniY - toMoveY);
				}
				
				MoveFromCenter((int)((movingRight ? 2145d : 2320d) * toMoveX / 34d / 2), (int)((movingTop ? 470d : 370d) * toMoveY / (movingTop ? 23d : 16d)));
				Click();
				Thread.Sleep(1000);
			}
			while (distanceOnMiniX != 0 || distanceOnMiniY != 0); 
		}
	}

	public void MoveFromCenter(int x = 0, int y = 0)
	{
		var newPosX = _sys.LAScreenX + _sys.LAScreenWidth / 2 + x;
		var newPosY = _sys.LAScreenY + _sys.LAScreenHeight / 2 + y;
		Move(newPosX, newPosY);
	}

	public void Move(int x, int y)
	{
		if (x > _sys.ResX) _logger.Log($"WARN - {nameof(MouseCtrl)} - cursor move to {x} is out of screen res x {_sys.ResX}");
		if (y > _sys.ResY) _logger.Log($"WARN - {nameof(MouseCtrl)} - cursor move to {y} is out of screen res y {_sys.ResY}");

		var cursosPos = Win32Api.GetCursorPosition();

		double pixelsToMoveX = x - cursosPos.X;
		double pixelsToMoveY = y - cursosPos.Y;

		_logger.Log($"Moving mouse from {cursosPos.X} / {cursosPos.Y} to {x} / {y}");

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
		Move(_sys.LAScreenX + _rnd.Next(10, 333), _sys.LAScreenY + _rnd.Next(10, 333));
	}

	public void Click()
	{
		_clickInputs[0].U.mi.dwFlags = Structures.MOUSEEVENTF.LEFTDOWN;
		_clickInputs[0].U.mi.time = 0;
		Win32Api.SendInput((uint) _clickInputs.Length, _clickInputs, Structures.INPUT.Size);
		Thread.Sleep(_rnd.Next(30, 60));
		_clickInputs[0].U.mi.dwFlags = Structures.MOUSEEVENTF.LEFTUP;
		Win32Api.SendInput((uint) _clickInputs.Length, _clickInputs, Structures.INPUT.Size);
	}

	private int _convertCoordinates(int coordinate, int resolution)
	{
		return 65536 * coordinate / resolution + 1;
	}
}
