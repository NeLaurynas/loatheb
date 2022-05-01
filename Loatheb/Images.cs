﻿using Emgu.CV;
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
	public Image<Bgr, byte> FishingGreyedOut;
	public Image<Bgr, byte> Dungeon1;
	public Image<Bgr, byte> Dungeon2;
	public Image<Bgr, byte> Dungeon3;
	public Image<Bgr, byte> Dungeon4;
	public Image<Bgr, byte> Dungeon5;
	public Image<Bgr, byte> Dungeon6;
	public Image<Bgr, byte> Dungeon7;
	public Image<Bgr, byte> Dungeon8;
	public Image<Bgr, byte> Dungeon9;
	public Image<Bgr, byte> Dungeon10;
	public Image<Bgr, byte> Dungeon11;
	public Image<Bgr, byte> Prog19;
	public Image<Bgr, byte> Prog20;
	public Image<Bgr, byte> Prog21;
	public Image<Bgr, byte> Qavail;
	public Image<Bgr, byte> Wavail;
	public Image<Bgr, byte> Eavail;
	public Image<Bgr, byte> Ravail;
	public Image<Bgr, byte> Aavail;
	public Image<Bgr, byte> Savail;
	public Image<Bgr, byte> Davail;
	public Image<Bgr, byte> Favail;
	public Image<Bgr, byte> DungeonNotSelected;
	public Image<Bgr, byte> DungeonSelected;
	public Image<Bgr, byte> AcceptBtn;
	public Image<Bgr, byte> ChaosDungeonWindowTitle;
	public Image<Bgr, byte> LeaveBtn;
	public Image<Bgr, byte> InsideDungeon;
	public Image<Bgr, byte> Loading;
	public Image<Bgr, byte> GearNeedsRepair1;
	public Image<Bgr, byte> GearNeedsRepair2;
	public Image<Bgr, byte> GearNeedsRepair3;
	public Image<Bgr, byte> GameMenu;

	private readonly Logger _logger;

	public Images(Logger logger)
	{
		_logger = logger;
	}

	public void Initialize()
	{
		_logger.Log("Initializing images");
		
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
		FishingGreyedOut = new Image<Bgr, Byte>("images/fishingGreyedOut.png");
		Dungeon1 = new Image<Bgr, Byte>("images/dungeon1.png");
		Dungeon2 = new Image<Bgr, Byte>("images/dungeon2.png");
		Dungeon3 = new Image<Bgr, Byte>("images/dungeon3.png");
		Dungeon4 = new Image<Bgr, Byte>("images/dungeon4.png");
		Dungeon5 = new Image<Bgr, Byte>("images/dungeon5.png");
		Dungeon6 = new Image<Bgr, Byte>("images/dungeon6.png");
		Dungeon7 = new Image<Bgr, Byte>("images/dungeon7.png");
		Dungeon8 = new Image<Bgr, Byte>("images/dungeon8.png");
		Dungeon9 = new Image<Bgr, Byte>("images/dungeon9.png");
		Dungeon10 = new Image<Bgr, Byte>("images/dungeon10.png");
		Dungeon11 = new Image<Bgr, Byte>("images/dungeon11.png");
		Prog19 = new Image<Bgr, Byte>("images/prog19.png");
		Prog20 = new Image<Bgr, Byte>("images/prog20.png");
		Prog21 = new Image<Bgr, Byte>("images/prog21.png");
		Qavail = new Image<Bgr, Byte>("images/Qavail.png");
		Wavail = new Image<Bgr, Byte>("images/Wavail.png");
		Eavail = new Image<Bgr, Byte>("images/Eavail.png");
		Ravail = new Image<Bgr, Byte>("images/Ravail.png");
		Aavail = new Image<Bgr, Byte>("images/Aavail.png");
		Savail = new Image<Bgr, Byte>("images/Savail.png");
		Davail = new Image<Bgr, Byte>("images/Davail.png");
		Favail = new Image<Bgr, Byte>("images/Favail.png");
		DungeonNotSelected = new Image<Bgr, Byte>("images/dungeonNotSelected.png");
		DungeonSelected = new Image<Bgr, Byte>("images/dungeonSelected.png");
		AcceptBtn = new Image<Bgr, Byte>("images/acceptBtn.png");
		ChaosDungeonWindowTitle = new Image<Bgr, Byte>("images/chaosDungeonWindowTitle.png");
		LeaveBtn = new Image<Bgr, Byte>("images/leaveBtn.png");
		InsideDungeon = new Image<Bgr, Byte>("images/insideDungeon.png");
		Loading = new Image<Bgr, Byte>("images/loading.png");
		GearNeedsRepair1 = new Image<Bgr, Byte>("images/gearNeedsRepair1.jpg");
		GearNeedsRepair2 = new Image<Bgr, Byte>("images/gearNeedsRepair2.png");
		GearNeedsRepair3 = new Image<Bgr, Byte>("images/gearNeedsRepair3.png");
		GameMenu = new Image<Bgr, Byte>("images/gameMenu.png");
	}
}
