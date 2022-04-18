using Emgu.CV;
using Emgu.CV.Structure;
namespace Loatheb;

public class Images
{
	public Image<Bgr, byte> EnterBtn;

	public void Initialize()
	{
		EnterBtn = new Image<Bgr, Byte>(@"E:\button.png");
	}
}
