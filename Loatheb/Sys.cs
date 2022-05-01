using System.Diagnostics;
using Loatheb.win32;
namespace Loatheb;

public class Sys
{
	private readonly Cfg _cfg;
	private readonly Logger _logger;
	
	private Structures.RECT _laScreenLoc;

	public Sys(Cfg cfg, Logger logger)
	{
		_cfg = cfg;
		_logger = logger;
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
		_logger.Log("Initializing system");
		// TODO: get monitor count, throw on more than one

		ResX = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CXSCREEN);
		ResY = Win32Api.GetSystemMetrics(Structures.SystemMetric.SM_CYSCREEN);

		RefreshLAWindowLocation();
	}

	public void RefreshLAWindowLocation()
	{
		var laHandlePtr = _getLAHandle();

		var screenLocSuccess = Win32Api.GetWindowRect(laHandlePtr.MainWindowHandle, out _laScreenLoc);
		if (!screenLocSuccess) throw new Exception("Couldn't get Lost Ark window lcoation");
		
		_logger.Log($"Refreshed LA Window location - {LAScreenX} / {LAScreenY}");
	}

	private Process _getLAHandle()
	{
		var laHandlePtr = Process.GetProcesses()
			.FirstOrDefault(x => x.ProcessName.ToLowerInvariant().StartsWith(_cfg.LAProcessName.ToLowerInvariant()));
		if (laHandlePtr is null) throw new Exception("Lost Ark process not found, is it running?");

		return laHandlePtr!;
	}
}
