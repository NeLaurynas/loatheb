using Loatheb;

Console.Write("Initializing cfg... ");
var cfg = new Cfg();
cfg.Initialize();
Console.WriteLine("done");

Console.Write("Initializing sys... ");
var sys = new Sys(cfg);
sys.Initialize();
Console.WriteLine("done");

Console.Write("Initializing mouseCtrl... ");
var mouseCtrl = new MouseCtrl(sys, cfg);
mouseCtrl.Initialize();
Console.WriteLine("done");

Console.Write("Initializing keyboardCtrl... ");
var kbdCtrl = new KbdCtrl(cfg);
kbdCtrl.Initialize();
Console.WriteLine("done");

Console.Write("Initializing images... ");
var images = new Images();
images.Initialize();
Console.WriteLine("done");

Console.Write("Initializing open CV... ");
var openCV = new OpenCV(sys);
Console.WriteLine("done");

Console.Write("Initializing repair module... ");
var repair = new Repairing(images, mouseCtrl, openCV, kbdCtrl);
Console.WriteLine("done");

Console.Write("Initializing fishing module... ");
var fishing = new Fishing(images, kbdCtrl, openCV, repair);
Console.WriteLine("done");

do
{
	Console.WriteLine("----");
	Console.WriteLine("Press [F] for fishing (be near water and with life skills open)");
	Console.WriteLine("Press [G] for grinding (be near chaos gate entrance)");
	Console.WriteLine("Press [X] to quit");

	var key = Console.ReadKey();
	switch (key.Key)
	{
		case ConsoleKey.F:
			await fishing.Start();
			break;
		case ConsoleKey.G:
			// grind
			break;
		case ConsoleKey.X:
			return 0;
	}
}
while (true);
