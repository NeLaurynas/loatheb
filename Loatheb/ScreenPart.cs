namespace Loatheb;

public enum ScreenPart
{
	Full,
	Left,
	Right,
	Top,
	Bottom
}
public class ScreenLoc
{
	public ScreenLoc(int x, int y, int width, int height)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
	}
	
	public int X { get; }
	public int Y { get; }
	public int Width { get; }
	public int Height { get; }
}
public static class ScreenLocations
{
	public static ScreenLoc Minimap => new(2220, 36, 314, 272);
	public static ScreenLoc DungeonProgressWindow => new(5, 30, 280, 300);
	public static ScreenLoc HPBarStart => new(935, 945, 40, 25);
	public static ScreenLoc MiddleOfTheScreen => new(700, 50, 1300, 850);
}
