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

	// mouseCtrl.Move(0, 0);

	using var cvImg = bitmap.ToImage<Bgr, Byte>();
	using var imgMatch = cvImg.MatchTemplate(images.EnterBtn, TemplateMatchingType.CcoeffNormed);

	var eh = new List<Rectangle>();

	using var m = new Matrix<float>(imgMatch.Rows, imgMatch.Cols);
	imgMatch.CopyTo(m);
	for (int i = 0; i < imgMatch.Rows; i++)
	{

		for (int j = 0; j < imgMatch.Cols; j++)
		{

			if (m[i, j] > 0.95)
			{
				eh.Add(new Rectangle(new Point(j, i), images.EnterBtn.Size));
				CvInvoke.Rectangle(cvImg, new Rectangle(new Point(j, i), images.EnterBtn.Size), new MCvScalar(0, 0, 255), 2);
			}
		}
	}

	if (eh.Count > 0)
	{
		Console.WriteLine("FOUND");
		var avgX = eh.Average(x => x.X);
		var avgY = eh.Average(x => x.Y);
		var avgHeight = eh.Average(x => x.Height);
		var avgWidth = eh.Average(x => x.Width);

		if (Math.Abs(avgX - eh.First().X) < 10 && Math.Abs(avgY - eh.First().Y) < 10)
		{
			Console.WriteLine($"rect X/Y averages smaller than 10! {avgX} / {avgY}");

			if (Math.Abs(avgHeight - eh.First().Height) < 10 && Math.Abs(avgWidth - eh.First().Width) < 10)
			{
				Console.WriteLine($"rect height/width averages smaller than 10! {avgHeight} x {avgWidth}");

				var posX = avgX + avgWidth / 2;
				var posY = avgY + avgHeight / 2;

				Console.WriteLine($"Took {sw.Elapsed.TotalMilliseconds} ms");

				mouseCtrl.Move((int)posX, (int)posY);
			}
		}
	}

	// Console.WriteLine("SAVING, results - " + eh.Count);
	// cvImg.Save(@"E:\result.png");
}
while (Console.ReadKey().Key != ConsoleKey.X);
