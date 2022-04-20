using Loatheb.win32;
namespace Loatheb;

public class KbdCtrl
{
	private readonly Random _rnd;
	private readonly Cfg _cfg;
	
	private Structures.INPUT[] _keyInput = null!;
	
	public KbdCtrl(Cfg cfg)
	{
		_rnd = new Random();
		_cfg = cfg;
	}

	public void Initialize()
	{
		_keyInput = new Structures.INPUT[1];

		var key = new Structures.KEYBDINPUT();
		var keyUpInputI = new Structures.INPUT();
		keyUpInputI.type = Structures.INPUT_KEYBOARD;
		keyUpInputI.U = new Structures.InputUnion {ki = key};
		_keyInput[0] = keyUpInputI;
	}

	public void PressKey(Structures.VirtualKeyShort key)
	{
		_keyInput[0].U.ki.dwFlags = 0;
		_keyInput[0].U.ki.wVk = key;
		_keyInput[0].U.ki.time = 0;

		Win32Api.SendInput((uint) _keyInput.Length, _keyInput, Structures.INPUT.Size);
		Thread.Sleep(_rnd.Next(30, 60));
		_keyInput[0].U.ki.dwFlags = Structures.KEYEVENTF.KEYUP;
		Win32Api.SendInput((uint) _keyInput.Length, _keyInput, Structures.INPUT.Size);
	}

	public void Escape()
	{
		PressKey(Structures.VirtualKeyShort.ESCAPE);
	}
	
	public void EscapeTwice()
	{
		Escape();
		Thread.Sleep(333);
		Escape();
	}
}
