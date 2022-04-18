using Loatheb.win32;
namespace Loatheb;

public class Sys
{
	public int ResX { get; set; }
	public int ResY { get; set; }

	public void Initialize()
	{
		// TODO: get monitor count, throw on

		ResX = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CXSCREEN);
		ResY = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CYSCREEN);
	}
}
