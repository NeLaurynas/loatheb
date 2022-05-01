using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
namespace Loatheb;

public class UIControl : INotifyPropertyChanged
{
	private readonly Logger _logger;
	private readonly LoathebForm _form;

	public UIControl(Logger logger, LoathebForm loathebForm)
	{
		_logger = logger;
		DebugImage = new Bitmap(1, 1);
		_form = loathebForm;
	}

	public Image DebugImage { get; set; }

	public void SetDebugImage(Bitmap image)
	{
		DebugImage = image;
		OnPropertyChanged(nameof(DebugImage));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		if (_form.InvokeRequired)
		{
			_form.Invoke(() =>
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			});
		}
		else
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
