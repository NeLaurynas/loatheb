using Loatheb.steps;
using Loatheb.win32;
namespace Loatheb;

public class Utils
{
	private readonly Logger _logger;
	private readonly KbdCtrl _kbdCtrl;
	private readonly MouseCtrl _mouseCtrl;
	private readonly OpenCV _openCv;
	private readonly Images _images;
	
	public Utils(Logger logger, KbdCtrl kbdCtrl, MouseCtrl mouseCtrl, OpenCV openCv, Images images)
	{
		_logger = logger;
		_kbdCtrl = kbdCtrl;
		_mouseCtrl = mouseCtrl;
		_openCv = openCv;
		_images = images;
	}

	public TryResettingUIStep CreateTryResettingUIStep(Step? nextStep = null)
	{
		return new TryResettingUIStep(nextStep);
	}

	public class TryResettingUIStep : Step
	{
		private readonly Step? _nextStep;
		
		public TryResettingUIStep(Step? nextStep)
		{
			_nextStep = nextStep;
			State.SleepDurationBeforeExecuting = 3000;
		}

		public override async Task<Step?> Execute()
		{
			_logger.Log("Trying to reset UI");
			DI.KbdCtrl.EscapeTwice();
			DI.MouseCtrl.SafePosition();
			Thread.Sleep(333);
			if (DI.OpenCV.IsMatching(DI.Images.GameMenu, 1150, 250, 400, 150, 0.9, true))
			{
				_logger.Log("Game menu is showing, sending ESC");
				DI.KbdCtrl.Escape();
			}
			_logger.Log("UI should be reset");

			return _nextStep;
		}
	}
	
	public void ActivateLAWindow()
	{
		DI.MouseCtrl.Move(DI.Sys.LAScreenX + 1, DI.Sys.LAScreenY + 1);
		DI.MouseCtrl.Click();
	}
}
