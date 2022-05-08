using System.ComponentModel;
using System.Runtime.CompilerServices;
using Loatheb.Annotations;
namespace Loatheb;

public class Logger : INotifyPropertyChanged
{
	public string Logs => String.Join(Environment.NewLine, _logs.Skip(_logIdx));

	private readonly string?[] _logs;
	private int _logIdx = 1999;
	private readonly LoathebForm _form;

	public Logger(LoathebForm loathebForm)
	{
		_logs = new string[2000];
		_form = loathebForm;
	}

	public void Log(string text)
	{
		// TODO: cfg to not log debug info
		if (text.StartsWith("C -")) return;
		
		_logs[_logIdx] = text;
		
		OnPropertyChanged(nameof(Logs));
		
		_logIdx--;
		if (_logIdx == 0)
		{
			_logIdx = 1999;
			for (var i = 0; i < _logs.Length; i++)
				_logs[i] = null;
		}
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
