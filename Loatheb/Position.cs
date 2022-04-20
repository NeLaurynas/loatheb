namespace Loatheb;

public enum Position
{
	Top,
	Right,
	Bottom,
	Left
}
public static class PositionExtension
{
	public static Position NextPosition(this Position position)
	{
		var inc = (int)position;
		inc++;
		inc %= 4;
		return (Position)inc;
	}
}
