using Loatheb.steps;
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

	// NEW ------------------------
	public async Task<bool> NeedsRepairingEquipment()
	{
		_logger.Log("Checking if gear needs repairing");
		var task1 = Task.Run(() => _openCv.IsMatching(_images.GearNeedsRepair1, 2000, 0, 150, 120));
		var task2 = Task.Run(() => _openCv.IsMatching(_images.GearNeedsRepair2, 2000, 0, 150, 120));
		var task3 = Task.Run(() => _openCv.IsMatching(_images.GearNeedsRepair3, 2000, 0, 150, 120));

		var res1 = await task1;
		var res2 = await task1;
		var res3 = await task1;

		return res1 || res2 || res3;
	}

	public Step OpenPetMenuForRepairGear()
	{
		return new OpenPetMenuStep();
	}

	public class OpenPetMenuStep : Step
	{
		public OpenPetMenuStep()
		{
			State.MaxIter = 3;
		}

		public override async Task<Step?> Execute()
		{
			_logger.Log("Opening Pet Menu for repairs");
			await Task.Yield();
			if (!false) // try opening pet menu, pretend to fail
			{
				return DI.Utils.CreateTryResettingUIStep(this);
			}
			
			return await base.Execute();
		}
	}

	// OLD -----------------------------
	#region OLD

	public bool NeedsRepairingToolOld()
	{
		_logger.Log("Checking if tools needs repairing");
		return _openCv.IsMatching(_images.ToolNeedsRepairing, 0.9, ScreenPart.Top);
	}

	public void OldRepairToolOld()
	{
		if (OldOpenPetFunctionOld())
		{
			if (OldOpenToolRepairOld())
			{
				if (OldClickRepairAllOld())
				{
					if (OldClickRepairAndOkOld())
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

	public bool OldOpenPetFunctionOld()
	{
		_mouseCtrl.SafePosition();
		_logger.Log("Opening pet function");
		_kbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_BracketRight);
		for (int i = 0; i < 5; i++)
		{
			if (OldPetFunctionShowingOld())
			{
				return true;
			}
			Thread.Sleep(400);
		}

		return false;
	}

	public bool OldPetFunctionShowingOld()
	{
		_logger.Log("Checking if pet function is showing");
		return _openCv.IsMatching(_images.PetFunction, 0.9, ScreenPart.Right);
	}

	public bool OldOpenToolRepairOld()
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
				if (OldRepairToolActiveOld())
				{
					return true;
				}
				Thread.Sleep(400);
			}
		}
		_logger.Log("Pet function repair tool not found");
		return false;
	}

	public bool OldRepairToolActiveOld()
	{
		_logger.Log("Checking if repair tool window is active");
		return _openCv.IsMatching(_images.RepairToolActive, 0.95, ScreenPart.Bottom) && _openCv.IsMatching(_images.RepairAll, 0.9, ScreenPart.Bottom);
	}

	public bool OldClickRepairAllOld()
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

	public bool OldClickRepairAndOkOld()
	{
		for (int i = 0; i < 5; i++)
		{
			_logger.Log("Checking if Repair All window is showing");
			if (_openCv.IsMatching(_images.RepairAll, 0.9, ScreenPart.Bottom))
			{
				return OldClickOkOld();
			}
			Thread.Sleep(400);
		}

		return false;
	}

	public bool OldClickOkOld()
	{
		var (result, locations) = _openCv.IsMatchingWhere(_images.OkBtn, 0.9, ScreenPart.Bottom);
		if (!result) return false;

		_mouseCtrl.Move(locations);
		Thread.Sleep(100);
		_mouseCtrl.Click();
		Thread.Sleep(500);
		return true;
	}

	#endregion
}
