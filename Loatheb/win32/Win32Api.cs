using System.Runtime.InteropServices;

namespace Loatheb.win32;

internal static class Win32Api
{
	[DllImport("user32.dll")]
	internal static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] Structures.INPUT[] pInputs, int cbSize);

	[DllImport("User32.dll")]
	internal static extern int GetSystemMetrics(Structures.SystemMetric smIndex);
}
