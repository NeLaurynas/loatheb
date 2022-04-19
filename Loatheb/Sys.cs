using System.Diagnostics;
using Loatheb.win32;
namespace Loatheb;

public class Sys
{
	private readonly Cfg _cfg;
	private Structures.RECT _laScreenLoc;

	public Sys(Cfg cfg)
	{
		_cfg = cfg;
	}
	public int ResX { get; set; }
	public int ResY { get; set; }
	public Structures.RECT LAScreenLoc => _laScreenLoc;

	public int LAScreenX => _laScreenLoc.Left;
	public int LAScreenY => _laScreenLoc.Top;
	public int LAScreenWidth => _laScreenLoc.Right - _laScreenLoc.Left;
	public int LAScreenHeight => _laScreenLoc.Bottom - _laScreenLoc.Top;
	
	public void Initialize()
	{
		// TODO: get monitor count, throw on more than one

		ResX = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CXSCREEN);
		ResY = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CYSCREEN);

		var laHandlePtr = Process.GetProcesses()
			.FirstOrDefault(x => x.ProcessName.ToLowerInvariant().StartsWith(_cfg.LAProcessName.ToLowerInvariant()));
		if (laHandlePtr is null) throw new Exception("Lost Ark process not found, is it running?");

		var screenLocSuccess = Win32Api.GetWindowRect(laHandlePtr.MainWindowHandle, out _laScreenLoc);
		if (!screenLocSuccess) throw new Exception("Couldn't get Lost Ark window lcoation");
	}
}
