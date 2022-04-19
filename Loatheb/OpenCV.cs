﻿using System.Drawing;
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
	
	public (double[] maxValues, Point[] maxLocations) Match(Image<Bgr, Byte> template)
	{
#pragma warning disable CA1416
		using var bitmap = new Bitmap(_sys.LAScreenWidth, _sys.LAScreenHeight);
		using var graphic = Graphics.FromImage(bitmap);
		graphic.CopyFromScreen(_sys.LAScreenX, _sys.LAScreenY, 0, 0, bitmap.Size, CopyPixelOperation.SourceCopy);
#pragma warning restore CA1416
		
		using var cvImg = bitmap.ToImage<Bgr, Byte>();
		using var imgMatch = cvImg.MatchTemplate(template, TemplateMatchingType.CcoeffNormed);
		
		imgMatch.MinMax(out _, out var maxValues, out _, out var maxLocations);
		for (var i = 0; i < maxLocations.Length; i++)
		{
			maxLocations[i].X += _sys.LAScreenX;
			maxLocations[i].Y += _sys.LAScreenY;
		}

		return (maxValues, maxLocations);
	}

	public bool IsMatching(Image<Bgr, Byte> template, double confidence)
	{
		var (result, _) = Match(template);
		return result.Length == 1 && result[0] >= confidence;
	}
}