using System.Diagnostics;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Loatheb;

var sys = new Sys();
Console.Write("Initializing sys... ");
sys.Initialize();
Console.WriteLine("done");

var cfg = new Cfg();
Console.Write("Initializing cfg... ");
cfg.Initialize();
Console.WriteLine("done");

var mouseCtrl = new MouseCtrl(sys, cfg);
Console.Write("Initializing mouseCtrl... ");
mouseCtrl.Initialize();
Console.WriteLine("done");

var images = new Images();
Console.Write("Initializing images... ");
images.Initialize();
Console.WriteLine("done");

do
{
	var sw = Stopwatch.StartNew();

#pragma warning disable CA1416
	using var bitmap = new Bitmap(sys.ResX, sys.ResY);
	using var graphic = Graphics.FromImage(bitmap);
	graphic.CopyFromScreen(0, 0, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
#pragma warning restore CA1416

	using var cvImg = bitmap.ToImage<Bgr, Byte>();
	using var imgMatch = cvImg.MatchTemplate(images.EnterBtn, TemplateMatchingType.CcoeffNormed);

	imgMatch.MinMax(out _, out var maxValues, out _, out var maxLocations);

	if (maxValues[0] > 0.9)
	{
		// This is a match. Do something with it, for example draw a rectangle around it.
		Rectangle match = new Rectangle(maxLocations[0], images.EnterBtn.Size);
		cvImg.Draw(match, new Bgr(Color.Red), 3);
	}

	Console.WriteLine($"Saving, time {sw.Elapsed.TotalMilliseconds:F1} ms");
	cvImg.Save(@"E:\result.png");
}
while (Console.ReadKey().Key != ConsoleKey.X);
