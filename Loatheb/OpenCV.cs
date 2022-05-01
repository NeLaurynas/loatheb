using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
namespace Loatheb;

public class OpenCV
{
	private readonly Sys _sys;
	private readonly Logger _logger;

	public OpenCV(Sys sys, Logger logger)
	{
		_sys = sys;
		_logger = logger;
	}

	public (double[] maxValues, Point[] maxLocations) Match(Image<Bgr, Byte> template, ScreenPart part = ScreenPart.Full)
	{
		var width = part switch
		{
			ScreenPart.Left => _sys.LAScreenWidth / 2,
			ScreenPart.Right => _sys.LAScreenWidth / 2,
			_ => 0
		};
		var height = part switch
		{
			ScreenPart.Top => _sys.LAScreenHeight / 2 - 210,
			ScreenPart.Bottom => _sys.LAScreenHeight / 2 - 210,
			_ => 0
		};
		var xPadding = part switch
		{
			ScreenPart.Right => _sys.LAScreenWidth / 2,
			_ => 0
		};
		var yPadding = part switch
		{
			ScreenPart.Bottom => _sys.LAScreenHeight / 2 - 210,
			_ => 0
		};

		return Match(template, xPadding, yPadding, width, height);
	}

	public Bitmap Temp(ScreenPart part = ScreenPart.Full)
	{
		var width = part switch
		{
			ScreenPart.Left => _sys.LAScreenWidth / 2,
			ScreenPart.Right => _sys.LAScreenWidth / 2,
			_ => 0
		};
		var height = part switch
		{
			ScreenPart.Top => _sys.LAScreenHeight / 2 - 210,
			ScreenPart.Bottom => _sys.LAScreenHeight / 2 - 210,
			_ => 0
		};
		var xPadding = part switch
		{
			ScreenPart.Right => _sys.LAScreenWidth / 2,
			_ => 0
		};
		var yPadding = part switch
		{
			ScreenPart.Bottom => _sys.LAScreenHeight / 2 - 210,
			_ => 0
		};

		return TakeScreenshot(xPadding, yPadding, width, height);
	}

	public Bitmap TakeScreenshot(int x, int y, int width = 0, int height = 0)
	{
		y += 210; // cut off top black bar
		
		if (width == 0)
			width = _sys.LAScreenWidth;
		if (height == 0)
			height = _sys.LAScreenHeight - 210 - 180;
		
#pragma warning disable CA1416
		var bitmap = new Bitmap(width, height);
		using var graphic = Graphics.FromImage(bitmap);
		graphic.CopyFromScreen(_sys.LAScreenX + x, _sys.LAScreenY + y, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
#pragma warning restore CA1416

		return bitmap;
	}
	
	public (double[] maxValues, Point[] maxLocations) Match(Image<Bgr, Byte> template, int x, int y, int width = 0, int height = 0)
	{
		using var bitmap = TakeScreenshot(x, y, width, height);
		using var cvImg = bitmap.ToImage<Bgr, Byte>();
		using var imgMatch = cvImg.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);

		imgMatch.MinMax(out _, out var maxValues, out _, out var maxLocations);
		for (var i = 0; i < maxLocations.Length; i++)
		{
			maxLocations[i].X += _sys.LAScreenX + x + template.Width / 2;
			maxLocations[i].Y += _sys.LAScreenY + y + 210 + template.Height / 2;
		}
		
		return (maxValues, maxLocations);
	}

	public bool IsMatching(Image<Bgr, Byte> template, double confidence = 0.9, ScreenPart part = ScreenPart.Full)
	{
		var (result, _) = Match(template, part);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return result.Length == 1 && result[0] >= confidence;
	}

	public (bool isMatching, Point[] maxLocations) IsMatchingWhere(Image<Bgr, Byte> template, double confidence = 0.9, ScreenPart part = ScreenPart.Full)
	{
		var (result, maxLocations) = Match(template, part);
		_logger.Log($"C - {result.Length}, V - {result.FirstOrDefault()}");
		return (result.Length == 1 && result[0] >= confidence, maxLocations);
	}
}
