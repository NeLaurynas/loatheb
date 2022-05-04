using Loatheb.win32;
namespace Loatheb.steps.repairSteps;

public class OpenPetMenuStep : StepBase
{
	public override StepStateBase State
	{
		get;
	}

	public OpenPetMenuStep()
	{
		State = new StepStateBase
		{
			MaxIter = 2
		};
	}

	public override async Task<StepBase?> Execute()
	{
		await Task.Yield();
		DI.Logger.Log("Opening Pet Menu for repairs");
		DI.KbdCtrl.PressKey(Structures.VirtualKeyShort.KEY_BracketRight);
			
		if (!Utils.TryUntilTrue(PetFunctionShowing))
		{
			return UtilSteps.CreateTryResettingUIStep(this);
		}

		ResetState();
		return RepairEquipmentSteps.OpenRepairMenuStep;
	}
	
	public bool PetFunctionShowing()
	{
		DI.Logger.Log("Checking if pet function is showing");
		return DI.OpenCV.IsMatching(DI.Images.PetFunction, 1500, 550, 220, 200);
	}
}
