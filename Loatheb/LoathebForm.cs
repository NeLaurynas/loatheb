using Loatheb.steps;
using Loatheb.steps.grindSteps;
namespace Loatheb
{
	public partial class LoathebForm : Form
	{
		private Logger _logger;
		private Cfg _cfg;
		private Sys _sys;
		private MouseCtrl _mouseCtrl;
		private KbdCtrl _kbdCtrl;
		private Images _images;
		private OpenCV _openCv;
		private Repairing _repairing;
		private Fishing _fishing;
		private Overlord _overlord;
		private UIControl _uiControl;

		public LoathebForm()
		{
			InitializeComponent();

			DI.Rnd = new Random();

			_logger = new Logger(this);
			logBox.DataBindings.Add("Text", _logger, nameof(Logger.Logs));
			DI.Logger = _logger;
			_logger.Log("Initialized logger");

			_cfg = new Cfg(_logger);
			_cfg.Initialize();
			DI.Cfg = _cfg;
			_logger.Log("Initialized configuration");

			_uiControl = new UIControl(_logger, this);
			imgDebug.DataBindings.Add("Image", _uiControl, nameof(UIControl.DebugImage));
			_logger.Log("Initialized UI control");

			_sys = new Sys(_cfg, _logger);
			_sys.Initialize();
			DI.Sys = _sys;
			_logger.Log("Initialized system");

			_mouseCtrl = new MouseCtrl(_sys, _cfg, _logger);
			_mouseCtrl.Initialize();
			DI.MouseCtrl = _mouseCtrl;
			_logger.Log("Initialized mouse controller");
			
			_kbdCtrl = new KbdCtrl(_cfg, _logger);
			_kbdCtrl.Initialize();
			DI.KbdCtrl = _kbdCtrl;
			_logger.Log("Initialized keyboard controller");

			_images = new Images(_logger);
			_images.Initialize();
			DI.Images = _images;
			_logger.Log("Initialized images");

			_openCv = new OpenCV(_sys, _logger, _uiControl);
			DI.OpenCV = _openCv;
			_logger.Log("Initialized open cv");

			_repairing = new Repairing(_images, _mouseCtrl, _openCv, _kbdCtrl, _logger);
			DI.Repairing = _repairing;
			_logger.Log("Initialized repairing module");

			_fishing = new Fishing(_images, _kbdCtrl, _openCv, _repairing, _mouseCtrl, _logger);
			_logger.Log("Initialized fishing module");

			_overlord = new Overlord(_logger, this);
			lblStatus2.DataBindings.Add("Text", _overlord, nameof(Overlord.Running));
			DI.Overlord = _overlord;
			_logger.Log("Overlord initialized");
		}

		private void refreshLAWindowLocBtn_Click(object sender, EventArgs e)
		{
			_sys.RefreshLAWindowLocation();
		}

        private void btnStart_Click(object sender, EventArgs e)
        {
			_overlord.Run();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
			_overlord.Stop();
        }

        private void btnTakeSS_Click(object sender, EventArgs e)
        {
			if (int.TryParse(txtX.Text, out var x) && int.TryParse(txtY.Text, out var y)
			    && int.TryParse(txtHeight.Text, out var height) && int.TryParse(txtWidth.Text, out var width))
            {
				_uiControl.SetDebugImage(_openCv.TakeScreenshot(x, y, width, height));
            }
			else
			{
				_logger.Log("Couldn't take screenshot - X/Y/Width/Height not a valid number");
			}
        }

        private void btnTmpMoveDog_Click(object sender, EventArgs e)
        {
			Utils.ActivateLAWindow();
			
			DI.Logger.Log("Trying to match dog on minimap");
			var (matches, location) = DI.OpenCV.IsMatchingWhere(DI.Images.TmpDog, ScreenLocations.Minimap, 0.85);

			if (matches)
			{
				DI.Logger.Log("Match");
				DI.MouseCtrl.MoveMinimapDistance(location);
			}
			else
			{
				DI.Logger.Log("No match");
			}
        }
    }
}
