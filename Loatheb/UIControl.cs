using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
namespace Loatheb;

public class UIControl : INotifyPropertyChanged
{
	private readonly Logger _logger;

	public UIControl(Logger logger)
	{
		_logger = logger;
		DebugImage = new Bitmap(1, 1);
	}

	public System.Drawing.Image DebugImage { get; set; }

	public void SetDebugImage(Bitmap image)
	{
		DebugImage = image;
		OnPropertyChanged(nameof(DebugImage));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
