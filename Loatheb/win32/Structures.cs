using System.Drawing;
using System.Runtime.InteropServices;

namespace Loatheb.win32;

public static class Structures
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct INPUT
	{
		internal uint type;
		internal InputUnion U;
		internal static int Size => Marshal.SizeOf(typeof(INPUT));
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public int Left; // x position of upper-left corner
		public int Top; // y position of upper-left corner
		public int Right; // x position of lower-right corner
		public int Bottom; // y position of lower-right corner
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int X;
		public int Y;

		public static implicit operator Point(POINT point)
		{
			return new Point(point.X, point.Y);
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	internal struct InputUnion
	{
		[FieldOffset(0)]
		internal MOUSEINPUT mi;
		[FieldOffset(0)]
		internal KEYBDINPUT ki;
		[FieldOffset(0)]
		internal HARDWAREINPUT hi;
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct MOUSEINPUT
	{
		internal int dx;
		internal int dy;
		internal int mouseData;
		internal MOUSEEVENTF dwFlags;
		internal uint time;
		internal UIntPtr dwExtraInfo;
	}

	internal const int INPUT_MOUSE = 0;
	internal const int INPUT_KEYBOARD = 1;
	internal const int INPUT_HARDWARE = 2;

	[StructLayout(LayoutKind.Sequential)]
	internal struct HARDWAREINPUT
	{
		internal int uMsg;
		internal short wParamL;
		internal short wParamH;
	}

	[Flags]
	internal enum MOUSEEVENTF : uint
	{
		ABSOLUTE = 0x8000,
		HWHEEL = 0x01000,
		MOVE = 0x0001,
		MOVE_NOCOALESCE = 0x2000,
		LEFTDOWN = 0x0002,
		LEFTUP = 0x0004,
		RIGHTDOWN = 0x0008,
		RIGHTUP = 0x0010,
		MIDDLEDOWN = 0x0020,
		MIDDLEUP = 0x0040,
		VIRTUALDESK = 0x4000,
		WHEEL = 0x0800,
		XDOWN = 0x0080,
		XUP = 0x0100
	}

	[StructLayout(LayoutKind.Sequential)]
	internal struct KEYBDINPUT
	{
		internal VirtualKeyShort wVk;
		internal ScanCodeShort wScan;
		internal KEYEVENTF dwFlags;
		internal int time;
		internal UIntPtr dwExtraInfo;
	}

	[Flags]
	internal enum KEYEVENTF : uint
	{
		EXTENDEDKEY = 0x0001,
		KEYUP = 0x0002,
		SCANCODE = 0x0008,
		UNICODE = 0x0004
	}

	public enum VirtualKeyShort : short
	{
		LBUTTON = 0x01,
		RBUTTON = 0x02,
		CANCEL = 0x03,
		MBUTTON = 0x04,
		XBUTTON1 = 0x05,
		XBUTTON2 = 0x06,
		BACK = 0x08,
		TAB = 0x09,
		CLEAR = 0x0C,
		RETURN = 0x0D,
		SHIFT = 0x10,
		CONTROL = 0x11,
		MENU = 0x12,
		PAUSE = 0x13,
		CAPITAL = 0x14,
		KANA = 0x15,
		HANGUL = 0x15,
		JUNJA = 0x17,
		FINAL = 0x18,
		HANJA = 0x19,
		KANJI = 0x19,
		ESCAPE = 0x1B,
		CONVERT = 0x1C,
		NONCONVERT = 0x1D,
		ACCEPT = 0x1E,
		MODECHANGE = 0x1F,
		SPACE = 0x20,
		PRIOR = 0x21,
		NEXT = 0x22,
		END = 0x23,
		HOME = 0x24,
		LEFT = 0x25,
		UP = 0x26,
		RIGHT = 0x27,
		DOWN = 0x28,
		SELECT = 0x29,
		PRINT = 0x2A,
		EXECUTE = 0x2B,
		SNAPSHOT = 0x2C,
		INSERT = 0x2D,
		DELETE = 0x2E,
		HELP = 0x2F,
		KEY_0 = 0x30,
		KEY_1 = 0x31,
		KEY_2 = 0x32,
		KEY_3 = 0x33,
		KEY_4 = 0x34,
		KEY_5 = 0x35,
		KEY_6 = 0x36,
		KEY_7 = 0x37,
		KEY_8 = 0x38,
		KEY_9 = 0x39,
		KEY_A = 0x41,
		KEY_B = 0x42,
		KEY_C = 0x43,
		KEY_D = 0x44,
		KEY_E = 0x45,
		KEY_F = 0x46,
		KEY_G = 0x47,
		KEY_H = 0x48,
		KEY_I = 0x49,
		KEY_J = 0x4A,
		KEY_K = 0x4B,
		KEY_L = 0x4C,
		KEY_M = 0x4D,
		KEY_N = 0x4E,
		KEY_O = 0x4F,
		KEY_P = 0x50,
		KEY_Q = 0x51,
		KEY_R = 0x52,
		KEY_S = 0x53,
		KEY_T = 0x54,
		KEY_U = 0x55,
		KEY_V = 0x56,
		KEY_W = 0x57,
		KEY_X = 0x58,
		KEY_Y = 0x59,
		KEY_Z = 0x5A,
		KEY_BracketRight = 0xDD,
		LWIN = 0x5B,
		RWIN = 0x5C,
		APPS = 0x5D,
		SLEEP = 0x5F,
		NUMPAD0 = 0x60,
		NUMPAD1 = 0x61,
		NUMPAD2 = 0x62,
		NUMPAD3 = 0x63,
		NUMPAD4 = 0x64,
		NUMPAD5 = 0x65,
		NUMPAD6 = 0x66,
		NUMPAD7 = 0x67,
		NUMPAD8 = 0x68,
		NUMPAD9 = 0x69,
		MULTIPLY = 0x6A,
		ADD = 0x6B,
		SEPARATOR = 0x6C,
		SUBTRACT = 0x6D,
		DECIMAL = 0x6E,
		DIVIDE = 0x6F,
		F1 = 0x70,
		F2 = 0x71,
		F3 = 0x72,
		F4 = 0x73,
		F5 = 0x74,
		F6 = 0x75,
		F7 = 0x76,
		F8 = 0x77,
		F9 = 0x78,
		F10 = 0x79,
		F11 = 0x7A,
		F12 = 0x7B,
		F13 = 0x7C,
		F14 = 0x7D,
		F15 = 0x7E,
		F16 = 0x7F,
		F17 = 0x80,
		F18 = 0x81,
		F19 = 0x82,
		F20 = 0x83,
		F21 = 0x84,
		F22 = 0x85,
		F23 = 0x86,
		F24 = 0x87,
		NUMLOCK = 0x90,
		SCROLL = 0x91,
		LSHIFT = 0xA0,
		RSHIFT = 0xA1,
		LCONTROL = 0xA2,
		RCONTROL = 0xA3,
		LMENU = 0xA4,
		RMENU = 0xA5,
		BROWSER_BACK = 0xA6,
		BROWSER_FORWARD = 0xA7,
		BROWSER_REFRESH = 0xA8,
		BROWSER_STOP = 0xA9,
		BROWSER_SEARCH = 0xAA,
		BROWSER_FAVORITES = 0xAB,
		BROWSER_HOME = 0xAC,
		VOLUME_MUTE = 0xAD,
		VOLUME_DOWN = 0xAE,
		VOLUME_UP = 0xAF,
		MEDIA_NEXT_TRACK = 0xB0,
		MEDIA_PREV_TRACK = 0xB1,
		MEDIA_STOP = 0xB2,
		MEDIA_PLAY_PAUSE = 0xB3,
		LAUNCH_MAIL = 0xB4,
		LAUNCH_MEDIA_SELECT = 0xB5,
		LAUNCH_APP1 = 0xB6,
		LAUNCH_APP2 = 0xB7,
		OEM_1 = 0xBA,
		OEM_PLUS = 0xBB,
		OEM_COMMA = 0xBC,
		OEM_MINUS = 0xBD,
		OEM_PERIOD = 0xBE,
		OEM_2 = 0xBF,
		OEM_3 = 0xC0,
		OEM_4 = 0xDB,
		OEM_5 = 0xDC,
		OEM_6 = 0xDD,
		OEM_7 = 0xDE,
		OEM_8 = 0xDF,
		OEM_102 = 0xE2,
		PROCESSKEY = 0xE5,
		PACKET = 0xE7,
		ATTN = 0xF6,
		CRSEL = 0xF7,
		EXSEL = 0xF8,
		EREOF = 0xF9,
		PLAY = 0xFA,
		ZOOM = 0xFB,
		NONAME = 0xFC,
		PA1 = 0xFD,
		OEM_CLEAR = 0xFE
	}
	public enum ScanCodeShort : short
	{
		LBUTTON = 0,
		RBUTTON = 0,
		CANCEL = 70,
		MBUTTON = 0,
		XBUTTON1 = 0,
		XBUTTON2 = 0,
		BACK = 14,
		TAB = 15,
		CLEAR = 76,
		RETURN = 28,
		SHIFT = 42,
		CONTROL = 29,
		MENU = 56,
		PAUSE = 0,
		CAPITAL = 58,
		KANA = 0,
		HANGUL = 0,
		JUNJA = 0,
		FINAL = 0,
		HANJA = 0,
		KANJI = 0,
		ESCAPE = 1,
		CONVERT = 0,
		NONCONVERT = 0,
		ACCEPT = 0,
		MODECHANGE = 0,
		SPACE = 57,
		PRIOR = 73,
		NEXT = 81,
		END = 79,
		HOME = 71,
		LEFT = 75,
		UP = 72,
		RIGHT = 77,
		DOWN = 80,
		SELECT = 0,
		PRINT = 0,
		EXECUTE = 0,
		SNAPSHOT = 84,
		INSERT = 82,
		DELETE = 83,
		HELP = 99,
		KEY_0 = 11,
		KEY_1 = 2,
		KEY_2 = 3,
		KEY_3 = 4,
		KEY_4 = 5,
		KEY_5 = 6,
		KEY_6 = 7,
		KEY_7 = 8,
		KEY_8 = 9,
		KEY_9 = 10,
		KEY_A = 30,
		KEY_B = 48,
		KEY_C = 46,
		KEY_D = 32,
		KEY_E = 18,
		KEY_F = 33,
		KEY_G = 34,
		KEY_H = 35,
		KEY_I = 23,
		KEY_J = 36,
		KEY_K = 37,
		KEY_L = 38,
		KEY_M = 50,
		KEY_N = 49,
		KEY_O = 24,
		KEY_P = 25,
		KEY_Q = 16,
		KEY_R = 19,
		KEY_S = 31,
		KEY_T = 20,
		KEY_U = 22,
		KEY_V = 47,
		KEY_W = 17,
		KEY_X = 45,
		KEY_Y = 21,
		KEY_Z = 44,
		LWIN = 91,
		RWIN = 92,
		APPS = 93,
		SLEEP = 95,
		NUMPAD0 = 82,
		NUMPAD1 = 79,
		NUMPAD2 = 80,
		NUMPAD3 = 81,
		NUMPAD4 = 75,
		NUMPAD5 = 76,
		NUMPAD6 = 77,
		NUMPAD7 = 71,
		NUMPAD8 = 72,
		NUMPAD9 = 73,
		MULTIPLY = 55,
		ADD = 78,
		SEPARATOR = 0,
		SUBTRACT = 74,
		DECIMAL = 83,
		DIVIDE = 53,
		F1 = 59,
		F2 = 60,
		F3 = 61,
		F4 = 62,
		F5 = 63,
		F6 = 64,
		F7 = 65,
		F8 = 66,
		F9 = 67,
		F10 = 68,
		F11 = 87,
		F12 = 88,
		F13 = 100,
		F14 = 101,
		F15 = 102,
		F16 = 103,
		F17 = 104,
		F18 = 105,
		F19 = 106,
		F20 = 107,
		F21 = 108,
		F22 = 109,
		F23 = 110,
		F24 = 118,
		NUMLOCK = 69,
		SCROLL = 70,
		LSHIFT = 42,
		RSHIFT = 54,
		LCONTROL = 29,
		RCONTROL = 29,
		LMENU = 56,
		RMENU = 56,
		BROWSER_BACK = 106,
		BROWSER_FORWARD = 105,
		BROWSER_REFRESH = 103,
		BROWSER_STOP = 104,
		BROWSER_SEARCH = 101,
		BROWSER_FAVORITES = 102,
		BROWSER_HOME = 50,
		VOLUME_MUTE = 32,
		VOLUME_DOWN = 46,
		VOLUME_UP = 48,
		MEDIA_NEXT_TRACK = 25,
		MEDIA_PREV_TRACK = 16,
		MEDIA_STOP = 36,
		MEDIA_PLAY_PAUSE = 34,
		LAUNCH_MAIL = 108,
		LAUNCH_MEDIA_SELECT = 109,
		LAUNCH_APP1 = 107,
		LAUNCH_APP2 = 33,
		OEM_1 = 39,
		OEM_PLUS = 13,
		OEM_COMMA = 51,
		OEM_MINUS = 12,
		OEM_PERIOD = 52,
		OEM_2 = 53,
		OEM_3 = 41,
		OEM_4 = 26,
		OEM_5 = 43,
		OEM_6 = 27,
		OEM_7 = 40,
		OEM_8 = 0,
		OEM_102 = 86,
		PROCESSKEY = 0,
		PACKET = 0,
		ATTN = 0,
		CRSEL = 0,
		EXSEL = 0,
		EREOF = 93,
		PLAY = 0,
		ZOOM = 98,
		NONAME = 0,
		PA1 = 0,
		OEM_CLEAR = 0,
	}

	internal enum SystemMetric
	{
		SM_CXSCREEN = 0, // 0x00
		SM_CYSCREEN = 1, // 0x01
		SM_CXVSCROLL = 2, // 0x02
		SM_CYHSCROLL = 3, // 0x03
		SM_CYCAPTION = 4, // 0x04
		SM_CXBORDER = 5, // 0x05
		SM_CYBORDER = 6, // 0x06
		SM_CXDLGFRAME = 7, // 0x07
		SM_CXFIXEDFRAME = 7, // 0x07
		SM_CYDLGFRAME = 8, // 0x08
		SM_CYFIXEDFRAME = 8, // 0x08
		SM_CYVTHUMB = 9, // 0x09
		SM_CXHTHUMB = 10, // 0x0A
		SM_CXICON = 11, // 0x0B
		SM_CYICON = 12, // 0x0C
		SM_CXCURSOR = 13, // 0x0D
		SM_CYCURSOR = 14, // 0x0E
		SM_CYMENU = 15, // 0x0F
		SM_CXFULLSCREEN = 16, // 0x10
		SM_CYFULLSCREEN = 17, // 0x11
		SM_CYKANJIWINDOW = 18, // 0x12
		SM_MOUSEPRESENT = 19, // 0x13
		SM_CYVSCROLL = 20, // 0x14
		SM_CXHSCROLL = 21, // 0x15
		SM_DEBUG = 22, // 0x16
		SM_SWAPBUTTON = 23, // 0x17
		SM_CXMIN = 28, // 0x1C
		SM_CYMIN = 29, // 0x1D
		SM_CXSIZE = 30, // 0x1E
		SM_CYSIZE = 31, // 0x1F
		SM_CXSIZEFRAME = 32, // 0x20
		SM_CXFRAME = 32, // 0x20
		SM_CYSIZEFRAME = 33, // 0x21
		SM_CYFRAME = 33, // 0x21
		SM_CXMINTRACK = 34, // 0x22
		SM_CYMINTRACK = 35, // 0x23
		SM_CXDOUBLECLK = 36, // 0x24
		SM_CYDOUBLECLK = 37, // 0x25
		SM_CXICONSPACING = 38, // 0x26
		SM_CYICONSPACING = 39, // 0x27
		SM_MENUDROPALIGNMENT = 40, // 0x28
		SM_PENWINDOWS = 41, // 0x29
		SM_DBCSENABLED = 42, // 0x2A
		SM_CMOUSEBUTTONS = 43, // 0x2B
		SM_SECURE = 44, // 0x2C
		SM_CXEDGE = 45, // 0x2D
		SM_CYEDGE = 46, // 0x2E
		SM_CXMINSPACING = 47, // 0x2F
		SM_CYMINSPACING = 48, // 0x30
		SM_CXSMICON = 49, // 0x31
		SM_CYSMICON = 50, // 0x32
		SM_CYSMCAPTION = 51, // 0x33
		SM_CXSMSIZE = 52, // 0x34
		SM_CYSMSIZE = 53, // 0x35
		SM_CXMENUSIZE = 54, // 0x36
		SM_CYMENUSIZE = 55, // 0x37
		SM_ARRANGE = 56, // 0x38
		SM_CXMINIMIZED = 57, // 0x39
		SM_CYMINIMIZED = 58, // 0x3A
		SM_CXMAXTRACK = 59, // 0x3B
		SM_CYMAXTRACK = 60, // 0x3C
		SM_CXMAXIMIZED = 61, // 0x3D
		SM_CYMAXIMIZED = 62, // 0x3E
		SM_NETWORK = 63, // 0x3F
		SM_CLEANBOOT = 67, // 0x43
		SM_CXDRAG = 68, // 0x44
		SM_CYDRAG = 69, // 0x45
		SM_SHOWSOUNDS = 70, // 0x46
		SM_CXMENUCHECK = 71, // 0x47
		SM_CYMENUCHECK = 72, // 0x48
		SM_SLOWMACHINE = 73, // 0x49
		SM_MIDEASTENABLED = 74, // 0x4A
		SM_MOUSEWHEELPRESENT = 75, // 0x4B
		SM_XVIRTUALSCREEN = 76, // 0x4C
		SM_YVIRTUALSCREEN = 77, // 0x4D
		SM_CXVIRTUALSCREEN = 78, // 0x4E
		SM_CYVIRTUALSCREEN = 79, // 0x4F
		SM_CMONITORS = 80, // 0x50
		SM_SAMEDISPLAYFORMAT = 81, // 0x51
		SM_IMMENABLED = 82, // 0x52
		SM_CXFOCUSBORDER = 83, // 0x53
		SM_CYFOCUSBORDER = 84, // 0x54
		SM_TABLETPC = 86, // 0x56
		SM_MEDIACENTER = 87, // 0x57
		SM_STARTER = 88, // 0x58
		SM_SERVERR2 = 89, // 0x59
		SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
		SM_CXPADDEDBORDER = 92, // 0x5C
		SM_DIGITIZER = 94, // 0x5E
		SM_MAXIMUMTOUCHES = 95, // 0x5F
		SM_REMOTESESSION = 0x1000, // 0x1000
		SM_SHUTTINGDOWN = 0x2000, // 0x2000
		SM_REMOTECONTROL = 0x2001, // 0x2001
		SM_CONVERTIBLESLATEMODE = 0x2003,
		SM_SYSTEMDOCKED = 0x2004,
	}
}
