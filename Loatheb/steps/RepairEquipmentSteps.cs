using Loatheb.steps.repairSteps;
namespace Loatheb.steps;

public static class RepairEquipmentSteps
{
	public static RepairEquipmentBegin RepairEquipmentBegin => new();
	public static OpenPetMenuStep OpenPetMenuStep => new();
	public static ClickRepairEquipmentStep ClickRepairEquipmentStep => new();
	public static OpenRepairMenuStep OpenRepairMenuStep => new();
}
