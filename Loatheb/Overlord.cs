using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
namespace Loatheb;

public class Overlord : INotifyPropertyChanged
{
	private readonly Logger _logger;

	public Overlord(Logger logger)
	{
		_logger = logger;
	}
	
	public bool Running { get; set; }

	public void Run()
	{
		Running = true;
		OnPropertyChanged(nameof(Running));
	}

	public void Stop()
	{
		Running = false;
		OnPropertyChanged(nameof(Running));
	}
	
	public event PropertyChangedEventHandler? PropertyChanged;
	
	[NotifyPropertyChangedInvocator]
	protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
