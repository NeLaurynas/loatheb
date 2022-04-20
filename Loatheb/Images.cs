using Emgu.CV;
using Emgu.CV.Structure;
namespace Loatheb;

public class Images
{
	public Image<Bgr, byte> EnterBtn;
	public Image<Bgr, byte> FishBuffReady;
	public Image<Bgr, byte> FishingInProgress;
	public Image<Bgr, byte> FishingMark1;
	public Image<Bgr, byte> FishingMark2;
	public Image<Bgr, byte> FishingMark3;
	public Image<Bgr, byte> FishingReady;
	public Image<Bgr, byte> LifePointsAvailable;
	public Image<Bgr, byte> RepairAll;
	public Image<Bgr, byte> RepairToolActive;
	public Image<Bgr, byte> ToolNeedsRepairing;
	public Image<Bgr, byte> OkBtn;
	public Image<Bgr, byte> RepairAllConfirmation;
	public Image<Bgr, byte> PetFunction;
	public Image<Bgr, byte> RepairToolBtn;

	public void Initialize()
	{
		EnterBtn = new Image<Bgr, Byte>("images/enterBtn.png");
		FishBuffReady = new Image<Bgr, Byte>("images/fishBuffReady.png");
		FishingInProgress = new Image<Bgr, Byte>("images/fishingInProgress.png");
		FishingMark1 = new Image<Bgr, Byte>("images/fishingMark1.png");
		FishingMark2 = new Image<Bgr, Byte>("images/fishingMark2.png");
		FishingMark3 = new Image<Bgr, Byte>("images/fishingMark3.png");
		FishingReady = new Image<Bgr, Byte>("images/fishingReady.png");
		LifePointsAvailable = new Image<Bgr, Byte>("images/lifePointsAvailable.png");
		RepairAll = new Image<Bgr, Byte>("images/repairAll.png");
		RepairToolActive = new Image<Bgr, Byte>("images/repairToolActive.png");
		ToolNeedsRepairing = new Image<Bgr, Byte>("images/toolNeedsRepairing.png");
		OkBtn = new Image<Bgr, Byte>("images/okBtn.png");
		RepairAllConfirmation = new Image<Bgr, Byte>("images/repairAllConfirmation.png");
		PetFunction = new Image<Bgr, Byte>("images/petFunction.png");
		RepairToolBtn = new Image<Bgr, Byte>("images/repairToolBtn.png");
		
	}
}
