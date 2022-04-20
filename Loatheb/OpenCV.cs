using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
namespace Loatheb;

public class OpenCV
{
	private readonly Sys _sys;

	public OpenCV(Sys sys)
	{
		_sys = sys;
	}

	public (double[] maxValues, Point[] maxLocations) Match(Image<Bgr, Byte> template, ScreenPart part = ScreenPart.Full)
	{
		var widthDivider = part switch
		{
			ScreenPart.Left => 2,
			ScreenPart.Right => 2,
			_ => 1
		};
		var heightDivider = part switch
		{
			ScreenPart.Top => 2,
			ScreenPart.Bottom => 2,
			_ => 1
		};
		var xPadding = part switch
		{
			ScreenPart.Right => _sys.LAScreenWidth / 2,
			_ => 0
		};
		var yPadding = part switch
		{
			ScreenPart.Bottom => _sys.LAScreenHeight / 2,
			_ => 180
		};
#pragma warning disable CA1416
		using var bitmap = new Bitmap(_sys.LAScreenWidth / widthDivider, (_sys.LAScreenHeight - 180 - 180) / heightDivider);
		using var graphic = Graphics.FromImage(bitmap);
		graphic.CopyFromScreen(_sys.LAScreenX + xPadding, _sys.LAScreenY + yPadding, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
#pragma warning restore CA1416

		using var cvImg = bitmap.ToImage<Bgr, Byte>();
		using var imgMatch = cvImg.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);

		imgMatch.MinMax(out _, out var maxValues, out _, out var maxLocations);
		for (var i = 0; i < maxLocations.Length; i++)
		{
			maxLocations[i].X += _sys.LAScreenX + xPadding + template.Width / 2;
			maxLocations[i].Y += _sys.LAScreenY + yPadding + template.Height / 2;
		}
		
		cvImg.Save(@"E:\result.png");

		return (maxValues, maxLocations);
	}

	public bool IsMatching(Image<Bgr, Byte> template, double confidence = 0.9, ScreenPart part = ScreenPart.Full)
	{
		var (result, _) = Match(template, part);
		Console.WriteLine($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] >= confidence;
	}

	public (bool isMatching, Point[] maxLocations) IsMatchingWhere(Image<Bgr, Byte> template, double confidence = 0.9, ScreenPart part = ScreenPart.Full)
	{
		var (result, maxLocations) = Match(template, part);
		Console.WriteLine($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return (result.Length == 1 && result[0] >= confidence, maxLocations);
	}
}
