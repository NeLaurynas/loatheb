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
		
	}
}
