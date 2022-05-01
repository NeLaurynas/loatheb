using Loatheb.win32;
namespace Loatheb;

public class Repairing
{
	private readonly Images _images;
	private readonly MouseCtrl _mouseCtrl;
	private readonly KbdCtrl _kbdCtrl;
	private readonly OpenCV _openCv;
	private readonly Logger _logger;
	
	public Repairing(Images images, MouseCtrl mouseCtrl, OpenCV openCv, KbdCtrl kbdCtrl, Logger logger)
	{
		_images = images;
		_mouseCtrl = mouseCtrl;
		_openCv = openCv;
		_kbdCtrl = kbdCtrl;
		_logger = logger;
	}

	public bool NeedsRepairingTool()
	{
		_logger.Log("Checking if tools need repairing");
		return _openCv.IsMatching(_images.ToolNeedsRepairing, 0.9, ScreenPart.Top);
	}

	public void RepairTool()
	{
		if (OpenPetFunction())
		{
			if (OpenToolRepair())
			{
				if (ClickRepairAll())
				{
					if (ClickRepairAndOk())
					{
						_logger.Log("Repair seems a great success, closing");
						Thread.Sleep(111);
						// _kbdCtrl.EscapeTwice();
						Thread.Sleep(222);
					}
					else
						_logger.Log("Failed repairing :<");
				}
				else
					_logger.Log("Repair All button not found :S");
			}
			else
				_logger.Log("Pet tool repair not showing :S");
		}
		else
			_logger.Log("Pet function is not showing, check if key is correct - ]");
	}

	public bool OpenPetFunction()
	{
		_mouseCtrl.SafePosition();
		_logger.Log("Opening pet function");
		_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_BracketRight);
		for (int i = 0; i < 5; i++)
		{
			if (PetFunctionShowing())
			{
				return true;
			}
			Thread.Sleep(400);
		}

		return false;
	}

	public bool PetFunctionShowing()
	{
		_logger.Log("Checking if pet function is showing");
		return _openCv.IsMatching(_images.PetFunction, 0.9, ScreenPart.Right);
	}

	public bool OpenToolRepair()
	{
		_logger.Log("Checking for tool repair button");
		var (matches, locations) = _openCv.IsMatchingWhere(_images.RepairToolBtn, 0.9, ScreenPart.Bottom);
		if (matches)
		{
			_mouseCtrl.Move(locations);
			Thread.Sleep(100);
			_mouseCtrl.Click();

			for (int i = 0; i < 5; i++)
			{
				if (RepairToolActive())
				{
					return true;
				}
				Thread.Sleep(400);
			}
		}
		_logger.Log("Pet function repair tool not found");
		return false;
	}

	public bool RepairToolActive()
	{
		_logger.Log("Checking if repair tool window is active");
		return _openCv.IsMatching(_images.RepairToolActive, 0.95, ScreenPart.Bottom) && _openCv.IsMatching(_images.RepairAll, 0.9, ScreenPart.Bottom);
	}

	public bool ClickRepairAll()
	{
		_logger.Log("Checking Repair All button");
		var (result, locations) = _openCv.IsMatchingWhere(_images.RepairAll, 0.9, ScreenPart.Bottom);
		if (result)
		{
			_mouseCtrl.Move(locations);
			Thread.Sleep(100);
			_mouseCtrl.Click();
			return true;
		}

		return false;
	}

	public bool ClickRepairAndOk()
	{
		for (int i = 0; i < 5; i++)
		{
			_logger.Log("Checking if Repair All window is showing");
			if (_openCv.IsMatching(_images.RepairAll, 0.9, ScreenPart.Bottom))
			{
				return ClickOk();
			}
			Thread.Sleep(400);
		}

		return false;
	}

	public bool ClickOk()
	{
		var (result, locations) = _openCv.IsMatchingWhere(_images.OkBtn, 0.9, ScreenPart.Bottom);
		if (!result) return false;
		
		_mouseCtrl.Move(locations);
		Thread.Sleep(100);
		_mouseCtrl.Click();
		Thread.Sleep(500);
		return true;
	}
}
