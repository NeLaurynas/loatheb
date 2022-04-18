using System.Drawing;
using System.Runtime.InteropServices;

namespace Loatheb.win32;

internal static class Win32Api
{
	[DllImport("user32.dll")]
	internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Structures.INPUT[] pInputs, int cbSize);

	[DllImport("User32.dll")]
	internal static extern int GetSystemMetrics(Structures.SystemMetric smIndex);

	[DllImport("user32.dll")]
	private static extern bool GetCursorPos(out Structures.POINT lpPoint);

	internal static Point GetCursorPosition()
	{
		Structures.POINT lpPoint;
		GetCursorPos(out lpPoint);
		// NOTE: If you need error handling
		// bool success = GetCursorPos(out lpPoint);
		// if (!success)

		return lpPoint;
	}
}
