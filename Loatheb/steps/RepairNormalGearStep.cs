namespace Loatheb.steps;

public class RepairNormalGearStep : Step
{
	public RepairNormalGearStep()
	{
		State.MaxIter = 1;
	}
	
	public override async Task<Step?> Execute()
	{
		if (await DI.Repairing.NeedsRepairingEquipment())
			return DI.Repairing.CreateOpenPetMenuForRepairGearStep();

		return null; // return Enter Chaos Dungeon Step
	}
	
}
